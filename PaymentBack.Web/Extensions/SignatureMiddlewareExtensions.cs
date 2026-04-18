using PaymentBack.Middleware;

namespace PaymentBack.Web.Extensions
{
    public static class SignatureMiddlewareExtensions
    {
        public static IApplicationBuilder UseSignatureForPostRequests(this IApplicationBuilder app)
        {
            return app.UseWhen(
                context => context.Request.Method == HttpMethods.Post,
                appBuilder =>
                {
                    appBuilder.UseMiddleware<SignatureMiddleware>();
                });
        }
    }
}
