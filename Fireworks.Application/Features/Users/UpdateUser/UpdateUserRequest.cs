using Ardalis.Result;
using MediatR;

namespace Fireworks.Application.Features.Users.UpdateUser;

public record UpdateUserRequest(Guid Id, string UserName, string? Email, string? PhoneNumber) : IRequest<Result>;