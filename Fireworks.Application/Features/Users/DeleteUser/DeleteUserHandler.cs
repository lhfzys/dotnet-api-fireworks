using Ardalis.Result;
using Fireworks.Domain.Identity.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Fireworks.Application.Features.Users.DeleteUser;

public class DeleteUserHandler(UserManager<ApplicationUser> userManager)
    : IRequestHandler<DeleteUserRequest, Result>
{
    public async Task<Result> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.Id.ToString());
        if (user is null) return Result.Invalid(new ValidationError(nameof(request.Id), "用户不存在"));
        var result = await userManager.DeleteAsync(user);
        return result.Succeeded
            ? Result.Success()
            : Result.Invalid(result.Errors.Select(e => new ValidationError(e.Code, e.Description)));
    }
}