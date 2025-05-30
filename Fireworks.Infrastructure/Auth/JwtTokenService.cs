using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Fireworks.Application.common;
using Fireworks.Application.common.Settings;
using Fireworks.Domain.Identity.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Fireworks.Infrastructure.Auth;

public class JwtTokenService(JwtSettings jwtSettings) : IJwtTokenService
{
    public string GenerateAccessToken(ApplicationUser user, IList<string> roles,IList<string> permissions)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName ?? ""),
            new(ClaimTypes.Email, user.Email ?? "")
        };
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        claims.AddRange(permissions.Select(p => new Claim("Permission", p)));
        // var rolePermissions = await dbContext.RolePermissions
        //     .Where(rp => roles.Contains(rp.Role.Name))
        //     .Include(rp => rp.Permission)
        //     .ToListAsync();
        // var permissions = rolePermissions.Select(rp => rp.Permission.Name).Distinct();
        // claims.AddRange(permissions.Select(p => new Claim("Permission", p)));
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(jwtSettings.ExpirationMinutes),
            signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public RefreshToken GenerateRefreshToken(string ipAddress)
    {
        return new RefreshToken
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            Expires = DateTime.UtcNow.AddDays(7),
            CreatedByIp = ipAddress
        };
    }

    public ClaimsPrincipal? ValidateAccessToken(string token)
    {
        throw new NotImplementedException();
    }
}