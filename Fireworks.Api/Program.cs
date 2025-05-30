using System.Security.Claims;
using Fireworks.Api.Configurations;
using Fireworks.Api.Configurations.ServiceRegistrations;
using Fireworks.Api.Endpoints;
using Fireworks.Api.Middleware;
using Fireworks.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddApplicationServices(builder.Configuration);


if (builder.Environment.EnvironmentName != "Testing")
{
    builder.Host.UseLoggingServices(builder.Configuration);
}

var app = builder.Build();
await ApplicationDbInitializer.InitializeAsync(app.Services);
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseSwaggerDocumentation();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapUserEndpoints();
app.MapRoleEndpoints();
app.MapUserRolesEndpoints();
app.MapLoginEndpoints();
app.MapRolePermissionEndpoints();

app.Run();