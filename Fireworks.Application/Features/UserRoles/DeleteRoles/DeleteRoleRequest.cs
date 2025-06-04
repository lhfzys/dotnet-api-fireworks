using Ardalis.Result;
using MediatR;

namespace Fireworks.Application.Features.UserRoles.AssignRole;

public record DeleteRoleRequest(Guid UserId, List<string> Roles): IRequest<Result>;
