using Fireworks.Domain.Entities;
using Fireworks.Domain.Identity.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fireworks.Application.common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<RefreshToken> RefreshTokens { get; set; }
    DbSet<UserLoginLog> UserLoginLogs { get; set; }
    DbSet<ApplicationUser> Users { get; set; }
    DbSet<ApplicationRole> Roles { get; set; }
    DbSet<Permission> Permissions { get; set; }
    DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}