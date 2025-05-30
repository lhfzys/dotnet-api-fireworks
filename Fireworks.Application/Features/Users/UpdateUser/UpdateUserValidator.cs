using FluentValidation;

namespace Fireworks.Application.Features.Users.UpdateUser;

public class UpdateUserValidator:AbstractValidator<UpdateUserRequest>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
    }
}