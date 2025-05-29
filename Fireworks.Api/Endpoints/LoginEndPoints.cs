using Fireworks.Api.Extensions;
using Fireworks.Application.Features.Auth.Login;
using MediatR;

namespace Fireworks.Api.Endpoints;

public static class LoginEndPoints
{
    public static void MapLoginEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/login").WithTags("Login");

        group.MapPost("/", async (IMediator mediator, LoginRequest request)
            => (await mediator.Send(request)).ToCustomMinimalApiResult());
    }
}