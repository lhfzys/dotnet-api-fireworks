using Fireworks.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Fireworks.Domain.Identity.Entities;

public class ApplicationRole:IdentityRole<Guid>
{
    public string? Description { get; set; } = string.Empty;
    
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}