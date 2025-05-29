using Ardalis.Result;
using Fireworks.Application.Features.Roles;
using MediatR;

namespace Fireworks.Application.Features.UserRoles.GetUserRoles;

public record GetUserRolesRequest(Guid UserId) : IRequest<Result<List<RoleResponse>>>;