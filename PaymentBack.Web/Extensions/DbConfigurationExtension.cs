using Microsoft.EntityFrameworkCore;
using PaymentBack.Domain.Enums;
using PaymentBack.Infrastructure.DbClient;

namespace PaymentBack.Web.Extensions
{
    public static class DbConfigurationExtension
    {
        public static void ConfigureDb(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("PaymentConnection");
            builder.Services.AddDbContext<PaymentDbContext>(options =>
                options.UseNpgsql(connectionString, 
                o => o.MapEnum<Status>("status")
                .UseAdminDatabase("postgres")
                .MigrationsAssembly("PaymentBack.Infrastructure")));
        }
    }
}
