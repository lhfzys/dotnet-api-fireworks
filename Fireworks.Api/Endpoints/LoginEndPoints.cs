using Fireworks.Api.Extensions;
using Fireworks.Application.Features.Auth.Login;
using Fireworks.Application.Features.Auth.logout;
using Fireworks.Application.Features.Auth.RefreshToken;
using MediatR;

namespace Fireworks.Api.Endpoints;

public static class LoginEndPoints
{
    public static void MapLoginEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/login").WithTags("Login");

        group.MapPost("/", async (IMediator mediator, LoginRequest request)
            => (await mediator.Send(request)).ToCustomMinimalApiResult());
        
        group.MapPost("/refresh-token", async (IMediator mediator, RefreshTokenRequest request) =>
            (await mediator.Send(request)).ToCustomMinimalApiResult());
        
        group.MapPost("/logout", async (IMediator mediator, LogoutRequest request) =>
            (await mediator.Send(request)).ToCustomMinimalApiResult());

    }
}