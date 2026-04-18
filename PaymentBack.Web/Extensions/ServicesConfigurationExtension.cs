using PaymentBack.Application.Services;
using PaymentBack.Infrastructure.Repositories;
using PaymentBack.Mapper;
namespace PaymentBack.Web.Extensions
{
    public static class ServicesConfigurationExtension
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddAutoMapper(cfg => {
                cfg.AddProfile<MapperProfile>();
            });
        }
    }
}
