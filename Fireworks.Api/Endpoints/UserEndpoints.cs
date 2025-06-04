using Fireworks.Api.Extensions;
using Fireworks.Api.Interfaces;
using Fireworks.Application.Features.Menus;
using Fireworks.Application.Features.Users.CreateUser;
using Fireworks.Application.Features.Users.DeleteUser;
using Fireworks.Application.Features.Users.GetAllUsers;
using Fireworks.Application.Features.Users.GetUserById;
using Fireworks.Application.Features.Users.UpdateUser;
using MediatR;

namespace Fireworks.Api.Endpoints;

public class UserEndPoints : IEndpointRegistrar
{
    public void MapEndpoints(IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/users").WithTags("User").RequireAuthorization();

        group.MapPost("/", async (IMediator mediator, CreateUserRequest request)
                => (await mediator.Send(request)).ToCustomMinimalApiResult())
            .RequireAuthorization("Permission:System:User");

        group.MapGet("/",
            async (IMediator mediator, [AsParameters] GetUsersRequest request) =>
            (await mediator.Send(request)).ToCustomMinimalApiResult());

        group.MapPut("/{id:guid}", async (Guid id, UpdateUserRequest req, IMediator mediator) =>
            (await mediator.Send(req with { Id = id })).ToCustomMinimalApiResult());

        group.MapDelete("/{id:guid}", async (Guid id, IMediator mediator) =>
            (await mediator.Send(new DeleteUserRequest(id))).ToCustomMinimalApiResult());

        group.MapGet("/{id:guid}", async (Guid id, IMediator mediator) =>
            (await mediator.Send(new GetUserByIdRequest(id))).ToCustomMinimalApiResult());

        group.MapGet("/menu", async (IMediator mediator) =>
                (await mediator.Send(new GetCurrentUserMenuRequest())).ToCustomMinimalApiResult())
            .RequireAuthorization()
            .WithName("GetCurrentUserMenu")
            .WithTags("User");
    }
}