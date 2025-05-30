using Ardalis.Result;
using MediatR;

namespace Fireworks.Application.Features.RolePermissions.UpdateRolePermissions;

public class UpdateRolePermissionsRequest:IRequest<Result>
{
    public Guid RoleId { get; set; }
    public List<Guid> PermissionIds { get; set; } = [];
}