using System.Text;
using Fireworks.Application.common.Authorization;
using Fireworks.Application.common.Constants;
using Fireworks.Application.common.Settings;
using Fireworks.Domain.Constants;
using Fireworks.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace Fireworks.Api.Configurations;

public static class AuthorizationServiceRegistration
{
    public static IServiceCollection AddAuthorizationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings?.Issuer,
                    ValidAudience = jwtSettings?.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings?.Key!))
                };
            });
        services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();;
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        services.AddAuthorization(options =>
        {
            foreach (var permission in PermissionConstants.All)
            {
                options.AddPolicy($"Permission:{permission}", policy =>
                    policy.RequireClaim("Permission", permission));
            }
            options.AddPolicy("RequireAdmin", policy => policy.RequireRole("Admin"));
        });
        return services;
    }
}