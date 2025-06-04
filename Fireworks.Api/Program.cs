using System.Reflection;
using Fireworks.Api.Configurations;
using Fireworks.Api.Interfaces;
using Fireworks.Api.Middleware;
using Fireworks.Infrastructure.Persistence;
using Fireworks.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddApplicationServices(builder.Configuration);


if (builder.Environment.EnvironmentName != "Testing")
{
    builder.Host.UseLoggingServices(builder.Configuration);
}
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
var app = builder.Build();
await PermissionSeeder.SeedAsync(app.Services);
await ApplicationDbInitializer.InitializeAsync(app.Services);

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseSwaggerDocumentation();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseDeveloperExceptionPage();
}
app.Use(async (context, next) =>
{
    try
    {
        await next.Invoke();
    }
    catch (Exception ex)
    {
        Console.WriteLine("🌋 Unhandled Exception: " + ex);
        throw;
    }
});
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
var endpointRegistrars = Assembly.GetExecutingAssembly()
    .GetTypes()
    .Where(t => typeof(IEndpointRegistrar).IsAssignableFrom(t) && t is { IsInterface: false, IsAbstract: false })
    .Select(Activator.CreateInstance)
    .Cast<IEndpointRegistrar>();
foreach (var registrar in endpointRegistrars)
{
    registrar.MapEndpoints(app);
}
app.Run();