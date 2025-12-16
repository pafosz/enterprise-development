using BikeRental.Application;
using BikeRental.Application.Contracts;
using BikeRental.Application.Contracts.Bicycles;
using BikeRental.Application.Contracts.Models;
using BikeRental.Application.Contracts.Rentals;
using BikeRental.Application.Contracts.Renters;
using BikeRental.Application.Services;
using BikeRental.Domain;
using BikeRental.Infrastructure.EfCore;
using BikeRental.Infrastructure.EfCore.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddSingleton<RentalFixture>();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new BikeRentalProfile());
});

builder.Services.AddScoped<IApplicationService<BicycleDto, BicycleCreateUpdateDto, int>, BicycleService>();
builder.Services.AddScoped<IApplicationService<ModelDto, ModelCreateUpdateDto, int>, ModelService>();
builder.Services.AddScoped<IApplicationService<RentalDto, RentalCreateUpdateDto, int>, RentalService>();
builder.Services.AddScoped<IApplicationService<RenterDto, RenterCreateUpdateDto, int>, RenterService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
builder.Services.AddTransient<IRepository<Bicycle, int>, BicycleRepository>();
builder.Services.AddTransient<IRepository<Model, int>, ModelRepository>();
builder.Services.AddTransient<IRepository<Rental, int>, RentalRepository>();
builder.Services.AddTransient<IRepository<Renter, int>, RenterRepository>();

builder.AddSqlServerDbContext<BikeRentalDbContext>("DatabaseConnection");

builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.UseInlineDefinitionsForEnums();

    var assemblies = AppDomain.CurrentDomain.GetAssemblies()
        .Where(a => a.GetName().Name!.StartsWith("BikeRental"))
        .Distinct();

    foreach (var assembly in assemblies)
    {
        var xmlFile = $"{assembly.GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (File.Exists(xmlPath))
            c.IncludeXmlComments(xmlPath);
    }

});

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<BikeRentalDbContext>();
    await db.Database.MigrateAsync();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
