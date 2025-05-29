using Ardalis.Result;
using Fireworks.Domain.Identity.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Fireworks.Application.Features.Roles.DeleteRole;

public class DeleteRoleHandler(RoleManager<ApplicationRole> roleManager, ILogger<DeleteRoleRequest> logger)
    : IRequestHandler<DeleteRoleRequest, Result>
{
    public async Task<Result> Handle(DeleteRoleRequest request, CancellationToken cancellationToken)
    {
        var role = await roleManager.FindByNameAsync(request.RoleName);
        if (role == null) return Result.Invalid(new ValidationError("roleName", "角色不存在"));
        var result = await roleManager.DeleteAsync(role);
        logger.LogInformation("删除角色成功");
        return result.Succeeded
            ? Result.Success()
            : Result.Invalid(result.Errors.Select(e => new ValidationError(e.Code, e.Description)));
    }
}