using Ardalis.Result;
using Fireworks.Application.common;
using Fireworks.Application.common.Interfaces;
using Fireworks.Application.common.Services;
using Fireworks.Domain.Identity.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Fireworks.Application.Features.Auth.Login;

public class LoginHandler(
    UserManager<ApplicationUser> userManager,
    IJwtTokenService jwtTokenService,
    IApplicationDbContext dbContext,
    IPermissionService permissionService,
    IClientIpService ipService,
    LoginLoggingService loginLogger)
    : IRequestHandler<LoginRequest, Result<AuthResponse>>
{
    public async Task<Result<AuthResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByNameAsync(request.UserName);
        if (user == null || !await userManager.CheckPasswordAsync(user, request.Password))
        {
            return Result.Invalid(new ValidationError(nameof(request.UserName), "用户名或密码错误"));
        }

        var roles = await userManager.GetRolesAsync(user);
        var permissions = await permissionService.GetPermissionsForUserAsync(user, cancellationToken);
        var accessToken = jwtTokenService.GenerateAccessToken(user, roles,permissions);
        var ip = ipService.GetClientIp();
        request.IpAddress = ip;
        var refreshToken = jwtTokenService.GenerateRefreshToken(ip);
        refreshToken.UserId = user.Id;
        dbContext.RefreshTokens.Add(refreshToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        await loginLogger.LogAsync(user, cancellationToken);
        return Result.Success(new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
            ExpiresAt = refreshToken.Expires
        });
    }
}