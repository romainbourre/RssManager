using RssManager.Api.Configurations;
using RssManager.Api.Extensions;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration.GetRequiredConnectionString("MySqlDatabase");

builder.Services
    .UseMySqlDatabase(connectionString)
    .AddProviders()
    .AddUseCases();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
