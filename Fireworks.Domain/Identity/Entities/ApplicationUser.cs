using Microsoft.AspNetCore.Identity;

namespace Fireworks.Domain.Identity.Entities;

public class ApplicationUser:IdentityUser<Guid>
{
    public string? ImageUrl { get; set; }
}