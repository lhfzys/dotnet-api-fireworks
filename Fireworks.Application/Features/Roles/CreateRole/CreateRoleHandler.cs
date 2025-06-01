using Ardalis.Result;
using Fireworks.Domain.Identity.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Fireworks.Application.Features.Roles.CreateRole;

public class CreateRoleHandler(RoleManager<ApplicationRole> roleManager, ILogger<CreateRoleRequest> logger)
    : IRequestHandler<CreateRoleRequest, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateRoleRequest request, CancellationToken cancellationToken)
    {
        var exists = await roleManager.RoleExistsAsync(request.RoleName);
        if (exists)
            return Result<Guid>.Invalid(
                new ValidationError("roleName", "角色已存在"));

        var role = new ApplicationRole
        {
            Name = request.RoleName
        };
        var result = await roleManager.CreateAsync(role);
        return result.Succeeded
            ? Result<Guid>.Success(role.Id)
            : Result<Guid>.Invalid(result.Errors.Select(e => new ValidationError(e.Code, e.Description)));
    }
}