using Ardalis.Result;
using MediatR;

namespace Fireworks.Application.Features.Users.GetUserById;

public record GetUserByIdRequest(Guid Id):IRequest<Result<UserResponse>>;