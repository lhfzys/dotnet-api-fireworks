using Ardalis.Result;
using Fireworks.Domain.Identity.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Fireworks.Application.Features.Auth.Login;

public class LoginHandler(
    UserManager<ApplicationUser> userManager,
    IJwtTokenService jwtTokenService,
    ApplicationDbContext dbContext)
    : IRequestHandler<LoginRequest, Result<AuthResponse>>
{
    public async Task<Result<AuthResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByNameAsync(request.UserName);
        if (user == null || !await userManager.CheckPasswordAsync(user, request.Password))
        {
            return Result.Invalid(new ValidationError(nameof(request.UserName), "Invalid credentials."));
        }
        var roles = await userManager.GetRolesAsync(user);
        var accessToken = jwtTokenService.GenerateAccessToken(user, roles);
        var refreshToken = jwtTokenService.GenerateRefreshToken(request.IpAddress);
        refreshToken.UserId = user.Id;
        dbContext.RefreshTokens.Add(refreshToken);
        await dbContext.SaveChangesAsync();
        return Result.Success(new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
            ExpiresIn = 900
        });
    }
}