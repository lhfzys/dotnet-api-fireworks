using System.Security.Claims;
using Fireworks.Application.common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Fireworks.Application.common.Services;

public class CurrentUserService(IHttpContextAccessor accessor) : ICurrentUserService
{
    public Guid Id => Guid.TryParse(accessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : Guid.Empty;
    public string? UserName => accessor.HttpContext?.User.Identity?.Name;
    public List<string> Roles =>
        accessor.HttpContext?.User.FindAll(ClaimTypes.Role)
            .Select(r => r.Value).ToList() ?? [];
}