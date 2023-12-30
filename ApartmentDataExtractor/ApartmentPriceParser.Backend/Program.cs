using ApartmentPriceParser.Backend.Database;
using ApartmentPriceParser.Backend.Database.Repositories;
using ApartmentPriceParser.Backend.Services;
using ApartmentPriceParser.Common.Models;
using ApartmentPriceParser.Common.Models.Providers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var config = new
{
    DatabaseVersion = builder.Configuration.GetValue<string>("Database:Version") ?? "10.4.19-MariaDB",
    DatabaseConnectionString = builder.Configuration.GetValue<string>("Database:ConnectionString")!
};
builder.Services
        .AddDbContextPool<BackendDatabaseContext>(x => x
        .UseMySql(config.DatabaseConnectionString, ServerVersion.Parse(config.DatabaseVersion))
        , 16);

builder.Services.AddScoped<AvitoApartmentDefinitionRepository>();
builder.Services.AddScoped<CianApartmentDefinitionRepository>();
builder.Services.AddScoped<ApartmentService>(sp => new ApartmentService(
    new Dictionary<ProviderType, IApartmentDefifnitionRepository>() 
    {
        { ProviderType.Cian, sp.GetRequiredService<CianApartmentDefinitionRepository>() },
        { ProviderType.Avito, sp.GetRequiredService<AvitoApartmentDefinitionRepository>() }
    }));
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
