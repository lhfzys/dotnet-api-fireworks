using FluentValidation;

namespace Fireworks.Application.Features.Auth.Login;

public class LoginValidator:AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleLevelCascadeMode = ClassLevelCascadeMode;
        RuleFor(x => x.UserName).NotEmpty().NotNull().MaximumLength(50);
        RuleFor(x => x.Password).NotEmpty().NotNull().MaximumLength(6);
    }
}