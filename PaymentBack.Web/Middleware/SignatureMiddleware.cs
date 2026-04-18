using System.Security.Cryptography;
using System.Text;

namespace PaymentBack.Web.Middleware
{
    public class SignatureMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _secretKey = "";

        public SignatureMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _secretKey = configuration["FrontEnd:SecretKey"] ?? throw new ArgumentNullException("SignatureSecretKey is not configured.");
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.EnableBuffering();

            if (!context.Request.Headers.TryGetValue("X-Signature", out var extractedSignature))
            {
                context.Response.StatusCode = 401;
                return;

            }

            using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;

            var computedSignature = ComputeHmacSha256(body, _secretKey);
            if (computedSignature != extractedSignature)
            {
                context.Response.StatusCode = 403;
                return;
            }
            await _next(context);
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
