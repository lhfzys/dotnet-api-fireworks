using FluentValidation;

namespace Fireworks.Application.Features.RolePermissions.UpdateRolePermissions;

public class UpdateRolePermissionsValidator:AbstractValidator<UpdateRolePermissionsRequest>
{
    public UpdateRolePermissionsValidator()
    {
        RuleFor(x => x.RoleId).NotEmpty();
        RuleFor(x => x.PermissionIds).NotEmpty().WithMessage("至少选择一个权限");
    }
}