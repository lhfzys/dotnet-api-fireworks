using FluentValidation;

namespace Fireworks.Application.Features.Roles.DeleteRole;

public class DeleteRoleValidator : AbstractValidator<DeleteRoleRequest>
{
    public DeleteRoleValidator()
    {
        RuleLevelCascadeMode = ClassLevelCascadeMode;
        RuleFor(x => x.RoleName).NotEmpty();
    }
}