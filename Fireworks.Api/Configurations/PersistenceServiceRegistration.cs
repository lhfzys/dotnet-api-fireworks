using Fireworks.Application.common;
using Fireworks.Application.common.Interfaces;
using Fireworks.Application.common.Services;
using Fireworks.Domain.Constants;
using Fireworks.Infrastructure.Auth;
using Fireworks.Infrastructure.Authorization;
using Fireworks.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Fireworks.Api.Configurations;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("PostgresSqlConnection"),
                npgsqlOptions =>
                {
                    npgsqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorCodesToAdd: null);
                });
        });
        services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<PermissionSynchronizationService>();
        services.AddScoped<IAuditLogService, AuditLogService>();
        return services;
    }
}