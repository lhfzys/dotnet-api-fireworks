using FluentValidation;

namespace Fireworks.Application.Features.Users.CreateUser;

public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleLevelCascadeMode = ClassLevelCascadeMode;

        RuleFor(x => x.UserName)
            .NotEmpty()
            .MaximumLength(50);
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);
    }
}