using Serilog;

namespace PaymentBack.Web.Extensions
{
    public static class LoggerConfigurationExtension
    {
        public static void ConfigureLogger(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
                .CreateLogger();

            builder.Host.UseSerilog();
        }
    }
}
