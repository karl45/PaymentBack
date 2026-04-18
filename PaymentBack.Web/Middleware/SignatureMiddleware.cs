using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace PaymentBack.Web.Middleware
{
    public class SignatureMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _secretKey = "";
        private readonly ILogger<SignatureMiddleware> _logger;

        public SignatureMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<SignatureMiddleware> logger)
        {
            _next = next;
            _secretKey = configuration["FrontEnd:SecretKey"] ?? throw new ArgumentNullException("SignatureSecretKey is not configured.");
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.EnableBuffering();
            var correlationId = Guid.NewGuid().ToString();

            using (_logger.BeginScope(new Dictionary<string, object> { ["CorrelationId"] = correlationId }))
            {
                var sw = Stopwatch.StartNew();

                try
                {
                    if (!context.Request.Headers.TryGetValue("X-Signature", out var extractedSignature))
                    {
                        context.Response.StatusCode = 401;
                        _logger.LogWarning("Запрос отклонен: отсутствует заголовок X-Signature. Путь: {Path}, IP: {IP}",
                                context.Request.Path, context.Connection.RemoteIpAddress);
                        return;
                    }

                    using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
                    var body = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0;

                    var computedSignature = ComputeHmacSha256(body, _secretKey);
                    if (computedSignature != extractedSignature)
                    {
                        context.Response.StatusCode = 403;
                        _logger.LogWarning("Запрос отклонен: неверная подпись. Путь: {Path}, IP: {IP}",
                            context.Request.Path, context.Connection.RemoteIpAddress);
                        return;
                    }
                    await _next(context);
                }
                finally
                {
                    sw.Stop();

                    _logger.LogInformation("Запрос {Method} {Path} завершен с кодом {StatusCode} за {Elapsed}ms",
                    context.Request.Method,
                    context.Request.Path,
                    context.Response.StatusCode,
                    sw.ElapsedMilliseconds);
                }

            }


        }

        private string ComputeHmacSha256(string data, string key)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var dataBytes = Encoding.UTF8.GetBytes(data);
            using var hmac = new HMACSHA256(keyBytes);
            var hash = hmac.ComputeHash(dataBytes);
            return Convert.ToHexString(hash).ToLower();
        }
    }
}
