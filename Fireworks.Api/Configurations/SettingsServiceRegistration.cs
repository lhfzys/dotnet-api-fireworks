using Fireworks.Application.common;
using Fireworks.Application.common.Interfaces;
using Fireworks.Application.common.Services;
using Fireworks.Application.common.Settings;
using Fireworks.Domain.Logging;
using Fireworks.Infrastructure.Behaviors;
using Fireworks.Infrastructure.Logging;
using Fireworks.Infrastructure.Permissions;
using Mapster;
using MediatR;
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
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<PermissionSynchronizationService>();
        services.AddScoped<IAuditLogger, AuditLogger>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuditBehavior<,>));
        return services;
    }
}