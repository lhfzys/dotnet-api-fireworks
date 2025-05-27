using Ardalis.Result;
using MediatR;

namespace Fireworks.Application.Features.Users.CreateUser;

public record CreateUserRequest : IRequest<Result<Guid>>
{
    public string UserName { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}   