using Microsoft.EntityFrameworkCore;
using PaymentBack.Infrastructure.DbClient;

namespace PaymentBack.Web.Extensions
{
    public static class DbMakeMigrationConfiguration
    {
        public static void ConfigureDbMakeMigration(this WebApplication app)
        {

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var db = services.GetRequiredService<PaymentDbContext>();
                    db.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Ошибка при миграции базы данных");
                }
            }
        }
    }
}
