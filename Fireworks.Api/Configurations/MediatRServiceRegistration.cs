

using Fireworks.Application.common;

namespace Fireworks.Api.Configurations;

public static class MediatRServiceRegistration
{
    public static IServiceCollection AddMediatRServices(this IServiceCollection services)
    {
        services.AddMediatR((config) =>
        {
            config.RegisterServicesFromAssemblyContaining<Application.IAssemblyMarker>();
            // config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });


        return services;
    }
}