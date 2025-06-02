using Ardalis.Result;
using Fireworks.Application.common;
using Fireworks.Application.common.Interfaces;
using Fireworks.Domain.Identity.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Fireworks.Application.Features.Auth.RefreshToken;

public class RefreshTokenHandler(
    IApplicationDbContext dbContext,
    IPermissionService permissionService,
    UserManager<ApplicationUser> userManager,
    IClientIpService ipService,
    IJwtTokenService jwtTokenService)
    : IRequestHandler<RefreshTokenRequest, Result<AuthResponse>>
{
    public async Task<Result<AuthResponse>> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var token = await dbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == request.Token, cancellationToken);
        if (token is not { IsActive: true })
        {
            return Result.Invalid(new ValidationError("RefreshToken", "Invalid or expired refresh token."));
        }

        var user = await userManager.FindByIdAsync(token.UserId.ToString());
        if (user == null) return Result.Error("User not found");

        var roles = await userManager.GetRolesAsync(user);
        var permissions = await permissionService.GetPermissionsForUserAsync(user, cancellationToken);
        var newAccessToken = jwtTokenService.GenerateAccessToken(user, roles,permissions);
        var ip = ipService.GetClientIp();
        request.IpAddress = ip;
        var newRefreshToken = jwtTokenService.GenerateRefreshToken(request.IpAddress);

        token.Revoked = DateTime.UtcNow;
        token.ReplacedByToken = newRefreshToken.Token;

        newRefreshToken.UserId = user.Id;
        dbContext.RefreshTokens.Add(newRefreshToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success(new AuthResponse
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken.Token,
            ExpiresAt = newRefreshToken.Expires
        });
    }
}