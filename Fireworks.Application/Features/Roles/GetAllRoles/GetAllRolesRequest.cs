using Ardalis.Result;
using MediatR;

namespace Fireworks.Application.Features.Roles.GetAllRoles;

public record GetAllRolesRequest():IRequest<Result<List<RoleResponse>>>;