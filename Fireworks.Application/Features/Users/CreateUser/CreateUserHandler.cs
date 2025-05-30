using Ardalis.Result;
using Fireworks.Domain.Identity.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Fireworks.Application.Features.Users.CreateUser;

public class CreateUserHandler(
    UserManager<ApplicationUser> userManager,
    ILogger<CreateUserRequest> logger)
    : IRequestHandler<CreateUserRequest, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        if (await userManager.FindByNameAsync(request.UserName) != null)
        {
            return Result<Guid>.Invalid(
                new ValidationError("username", "用户名已被占用"));
        }

        var user = new ApplicationUser
        {
            UserName = request.UserName,
        };

        var createResult = await userManager.CreateAsync(user, request.Password);
        if (!createResult.Succeeded)
            Result.Invalid(createResult.Errors.Select(e => new ValidationError(e.Code, e.Description)));

        logger.LogInformation("用户 {UserId} 创建成功", user.Id);
        return Result.Success(user.Id);
    }
}