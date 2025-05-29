using Fireworks.Domain.Common;

namespace Fireworks.Domain.Identity.Entities;

public class RefreshToken:BaseEntity<Guid>
{
    public string Token { get; set; } = null!;
    public DateTime Expires { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public string CreatedByIp { get; set; } = null!;
    public DateTime? Revoked { get; set; }
    public string? ReplacedByToken { get; set; }
    
    public bool IsActive => Revoked == null && !IsExpired;
    public bool IsExpired => Expires <= DateTime.UtcNow;
    
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;
}