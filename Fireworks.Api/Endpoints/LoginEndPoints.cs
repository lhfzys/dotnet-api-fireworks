using Fireworks.Api.Extensions;
using Fireworks.Api.Interfaces;
using Fireworks.Application.Features.Auth.Login;
using Fireworks.Application.Features.Auth.logout;
using Fireworks.Application.Features.Auth.RefreshToken;
using MediatR;

namespace Fireworks.Api.Endpoints;

public class LoginEndPoints : IEndpointRegistrar
{
    public void MapEndpoints(IEndpointRouteBuilder builder)
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