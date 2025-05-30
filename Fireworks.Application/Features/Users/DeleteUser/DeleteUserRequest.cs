using Ardalis.Result;
using MediatR;

namespace Fireworks.Application.Features.Users.DeleteUser;

public record DeleteUserRequest(Guid Id):IRequest<Result>;