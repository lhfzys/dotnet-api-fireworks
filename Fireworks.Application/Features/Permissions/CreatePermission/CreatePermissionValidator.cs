using FluentValidation;

namespace Fireworks.Application.Features.Permissions.CreatePermission;

public class CreatePermissionValidator: AbstractValidator<CreatePermissionRequest>
{
    public CreatePermissionValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Code).NotEmpty().MaximumLength(100);
    }
}