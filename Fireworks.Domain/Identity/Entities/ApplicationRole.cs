using Microsoft.AspNetCore.Identity;

namespace Fireworks.Domain.Identity.Entities;

public class ApplicationRole:IdentityRole<Guid>
{
    public string? Description { get; set; }
}