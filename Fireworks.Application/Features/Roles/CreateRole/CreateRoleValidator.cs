using FluentValidation;

namespace Fireworks.Application.Features.Roles.CreateRole;

public class CreateRoleValidator : AbstractValidator<CreateRoleRequest>
{
    public CreateRoleValidator()
    {
        RuleLevelCascadeMode = ClassLevelCascadeMode;
        RuleFor(x => x.RoleName).NotEmpty();
    }
}