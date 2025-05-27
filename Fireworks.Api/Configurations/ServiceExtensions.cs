using Fireworks.Api.Configurations.ServiceRegistrations;

namespace Fireworks.Api.Configurations;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddPersistenceServices(configuration)
            .AddIdentityServices(configuration)
            .AddSwaggerServices()
            .AddMediatRServices()
            .AddApplicationServices();

        return services;
    }
}