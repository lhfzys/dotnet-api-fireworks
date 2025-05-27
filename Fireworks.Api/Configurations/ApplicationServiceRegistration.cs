using Fireworks.Application.common;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fireworks.Api.Configurations.ServiceRegistrations;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<Application.IAssemblyMarker>();

        services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services;
    }
}