using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using PaymentBack.Application.Services;
using PaymentBack.Infrastructure.Repositories;
using PaymentBack.Mapper;
using System.Reflection;
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

            builder.Services.AddFluentValidationAutoValidation();

            var validatorsAssembly = Assembly.Load("PaymentBack.Application");
            builder.Services.AddValidatorsFromAssembly(validatorsAssembly);
            builder.Services.AddSwaggerGen();
            builder.Services.AddFluentValidationRulesToSwagger();
        }
    }
}
