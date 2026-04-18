namespace PaymentBack.Web.Extensions
{
    public static class CorsConfigurationExtension
    {
        public static void ConfigureCors(this WebApplicationBuilder builder)
        {
            var client_url = builder.Configuration["FrontEnd:API_URL"];

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowClient", policy =>
                {
                    policy.WithOrigins(client_url)
                          .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });
        }
    }
}
