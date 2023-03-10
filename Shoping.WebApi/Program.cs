using Shoping.Application;
using Shoping.Domain;
using Shoping.Infrastructure;
using Shoping.Persistence;
using Shoping.WebApi;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Identity;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog();

builder.Services.AddWebApi();
builder.Services.AddApplicationCore();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructure();
builder.Services.AddSecurity(builder.Configuration);
builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.ApplicationInsights(app.Services.GetRequiredService<TelemetryConfiguration>(), TelemetryConverter.Traces)
    // .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();



try
{
    Log.Information("Iniciando Web API");

    await SeedProducts();

    Log.Information("Corriendo en:");
    Log.Information("https://localhost:7113");
    Log.Information("http://localhost:5144");

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");

    return;
}
finally
{
    Log.CloseAndFlush();
}


async Task SeedProducts()
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    context.Database.EnsureCreated();

    if (app.Environment.IsDevelopment())
    {
        await AppDbContextSeed.SeedDataAsync(context);
        await AppDbContextSeed.SeedUsersAsync(userManager, roleManager);
    }

}