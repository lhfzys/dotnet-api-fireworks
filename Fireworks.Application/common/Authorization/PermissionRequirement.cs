using Microsoft.AspNetCore.Authorization;

namespace Fireworks.Application.common.Authorization;

public class PermissionRequirement(string permissionCode) : IAuthorizationRequirement
{
    public string PermissionCode { get; } = permissionCode;
}