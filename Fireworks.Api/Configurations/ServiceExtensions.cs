namespace Fireworks.Api.Configurations;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddPersistenceServices(configuration)
            .AddAuthorizationServices(configuration)
            .AddIdentityServices(configuration)
            .AddSettingServices(configuration)
            .AddSwaggerServices()
            .AddMediatRServices()
            .AddApplicationServices();

        return services;
    }
}