using Ardalis.Result;
using MediatR;

namespace Fireworks.Application.Features.Users.GetCurrentUser;

public record GetCurrentUserRequest():IRequest<Result<MeResponse>>;