using Ardalis.Result;
using Fireworks.Application.Features.Roles;
using Fireworks.Domain.Identity.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Fireworks.Application.Features.UserRoles.GetUserRoles;

public class GetUserRolesHandler(UserManager<ApplicationUser> userManager)
    : IRequestHandler<GetUserRolesRequest, Result<List<RoleResponse>>>
{
    public async Task<Result<List<RoleResponse>>> Handle(GetUserRolesRequest request,
        CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null) return Result.Invalid(new ValidationError(nameof(request.UserId), "用户不存在"));
        var roles = userManager.GetRolesAsync(user);
        return Result.Success(roles.Result.Select(x => new RoleResponse { RoleName = x }).ToList());
    }
}