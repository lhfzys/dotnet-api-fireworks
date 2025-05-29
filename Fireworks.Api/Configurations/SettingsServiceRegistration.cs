using Fireworks.Application.common;
using Fireworks.Application.common.Settings;
using Microsoft.Extensions.Options;

namespace Fireworks.Api.Configurations;

public static class SettingsServiceRegistration
{
    public static IServiceCollection AddSettingServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<JwtSettings>>().Value);
        services.AddHttpContextAccessor();
        services.AddScoped<IClientIpService, ClientIpService>();
        services.AddScoped<LoginLoggingService>();
        return services;
    }
}