using Fireworks.Application.common;
using Fireworks.Application.common.Interfaces;
using Fireworks.Application.common.Services;
using Fireworks.Application.common.Settings;
using Mapster;
using Microsoft.Extensions.Options;

namespace Fireworks.Api.Configurations;

public static class SettingsServiceRegistration
{
    public static IServiceCollection AddSettingServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMapster();
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<JwtSettings>>().Value);
        services.AddHttpContextAccessor();
        services.AddScoped<IClientIpService, ClientIpService>();
        services.AddScoped<LoginLoggingService>();
        services.AddScoped<IPermissionService,PermissionService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        return services;
    }
}