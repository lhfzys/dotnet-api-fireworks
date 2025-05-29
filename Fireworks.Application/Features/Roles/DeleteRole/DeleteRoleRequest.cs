using Ardalis.Result;
using MediatR;

namespace Fireworks.Application.Features.Roles.DeleteRole;

public record DeleteRoleRequest(string RoleName) : IRequest<Result>;