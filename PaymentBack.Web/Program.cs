using PaymentBack.Web.Extensions;

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
app.UseSignatureForPostRequests();

app.MapControllers();

app.Run();
