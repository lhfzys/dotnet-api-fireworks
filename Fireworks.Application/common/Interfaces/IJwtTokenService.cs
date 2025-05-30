using System.Security.Claims;
using Fireworks.Domain.Identity.Entities;

namespace Fireworks.Application.common;

public interface IJwtTokenService
{
    string GenerateAccessToken(ApplicationUser user, IList<string> roles,IList<string> permissions);
    RefreshToken GenerateRefreshToken(string ipAddress);
    ClaimsPrincipal? ValidateAccessToken(string token);
}