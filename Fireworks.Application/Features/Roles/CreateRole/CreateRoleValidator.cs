using FluentValidation;

namespace Fireworks.Application.Features.Roles;

public class CreateRoleValidator : AbstractValidator<CreateRoleRequest>
{
    public CreateRoleValidator()
    {
        RuleLevelCascadeMode = ClassLevelCascadeMode;
        RuleFor(x => x.RoleName).NotEmpty();
    }
}