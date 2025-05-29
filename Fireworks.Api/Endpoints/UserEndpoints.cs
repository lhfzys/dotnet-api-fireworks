using Fireworks.Api.Extensions;
using Fireworks.Application.Features.Users.CreateUser;
using MediatR;

namespace Fireworks.Api.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/users").WithTags("User").RequireAuthorization();

        group.MapPost("/", async (IMediator mediator, CreateUserRequest request)
            => (await mediator.Send(request)).ToCustomMinimalApiResult());
    }
}