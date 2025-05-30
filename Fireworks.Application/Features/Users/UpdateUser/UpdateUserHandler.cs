using Ardalis.Result;
using Fireworks.Domain.Identity.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Fireworks.Application.Features.Users.UpdateUser;

public class UpdateUserHandler(UserManager<ApplicationUser> userManager)
    : IRequestHandler<UpdateUserRequest, Result>
{
    public async Task<Result> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.Id.ToString());
        if (user is null) return Result.Invalid(new ValidationError(nameof(request.Id), "用户不存在"));
        if (!string.IsNullOrEmpty(request.Email))
        {
            user.Email = request.Email;
        }

        if (!string.IsNullOrEmpty(request.UserName))
        {
            user.UserName = request.UserName;
        }

        if (!string.IsNullOrEmpty(request.PhoneNumber))
        {
            user.PhoneNumber = request.PhoneNumber;
        }

        var result = await userManager.UpdateAsync(user);
        return result.Succeeded
            ? Result.Success()
            : Result.Invalid(result.Errors.Select(e => new ValidationError(e.Code, e.Description)));
    }
}