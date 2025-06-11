using Ardalis.Result;
using Fireworks.Application.common.Interfaces;
using Fireworks.Domain.Identity.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Fireworks.Application.Features.Users.GetCurrentUser;

public class GetCurrentUserHandler(
    IApplicationDbContext context,
    UserManager<ApplicationUser> userManager,
    ICurrentUserService currentUser)
    : IRequestHandler<GetCurrentUserRequest, Result<MeResponse>>
{
    public async Task<Result<MeResponse>> Handle(GetCurrentUserRequest request, CancellationToken cancellationToken)
    {
        var userId = currentUser.Id;
        if (userId == Guid.Empty) return Result.Invalid(new ValidationError(nameof(userId), "未找到用户信息"));
        var user = await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        var roleNames = currentUser.Roles;
        var permissions = currentUser.Permissions;
        return Result.Success(new MeResponse
        {
            Id = userId,
            UserName = currentUser.UserName,
            Roles = roleNames.ToArray(),
            Permissions = permissions.ToArray()
        });
    }
}