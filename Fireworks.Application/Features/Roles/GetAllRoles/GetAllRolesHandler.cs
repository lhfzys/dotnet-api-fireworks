using Ardalis.Result;
using Fireworks.Application.Features.Roles.CreateRole;
using Fireworks.Domain.Identity.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Fireworks.Application.Features.Roles.GetAllRoles;

public class GetAllRolesHandler(RoleManager<ApplicationRole> roleManager, ILogger<CreateRoleRequest> logger)
    : IRequestHandler<GetAllRolesRequest, Result<List<RoleResponse>>>
{
    public async Task<Result<List<RoleResponse>>> Handle(GetAllRolesRequest request,
        CancellationToken cancellationToken)
    {
        var roles = await roleManager.Roles.Select(r => new RoleResponse() { RoleId = r.Id, RoleName = r.Name })
            .ToListAsync(cancellationToken: cancellationToken);
        logger.LogInformation($"查询角色列表");
        return Result.Success(roles);
    }
}