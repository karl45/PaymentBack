using Microsoft.EntityFrameworkCore;
using PaymentBack.Extensions;
using PaymentBack.Middleware;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();


builder.ConfigureDb();
builder.ConfigureServices();
builder.ConfigureCors();

var app = builder.Build();
app.ConfigureDbMakeMigration();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowClient");
app.UseWhen(context => context.Request.Method == "POST", appBuilder =>
{
    appBuilder.UseMiddleware<SignatureMiddleware>();
});
app.MapControllers();

app.Run();
