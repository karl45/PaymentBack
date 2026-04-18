using PaymentBack.Application.DTO.CreatePaymentDto;
using PaymentBack.Application.Services;
using PaymentBack.Domain.Enums;
using System.Security.Cryptography;
using System.Text;

namespace PaymentBack.Middleware
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

        public async Task InvokeAsync(HttpContext context, IPaymentService _paymentService)
        {
            context.Request.EnableBuffering();

            if (!context.Request.Headers.TryGetValue("X-Signature", out var extractedSignature))
            {
                context.Response.StatusCode = 401;
                var paymentModel = await context.Request.ReadFromJsonAsync<CreatePaymentDtoRequest>();
                paymentModel.Payment.Status = Status.Rejected;
                await _paymentService.CreatePaymentAsync(paymentModel, context.RequestAborted);
                return;

            }

            using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;

            var computedSignature = ComputeHmacSha256(body, _secretKey);
            if (computedSignature != extractedSignature)
            {
                context.Response.StatusCode = 403;
                var paymentModel = await context.Request.ReadFromJsonAsync<CreatePaymentDtoRequest>();
                paymentModel.Payment.Status = Status.Rejected;
                await _paymentService.CreatePaymentAsync(paymentModel, context.RequestAborted);
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
