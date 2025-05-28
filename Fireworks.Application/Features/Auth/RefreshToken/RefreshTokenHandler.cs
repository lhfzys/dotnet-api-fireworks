using Ardalis.Result;
using Fireworks.Domain.Identity.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Fireworks.Application.Features.Auth.RefreshToken;

public class RefreshTokenHandler(
    ApplicationDbContext dbContext,
    UserManager<ApplicationUser> userManager,
    IJwtTokenService jwtTokenService)
    : IRequestHandler<RefreshTokenRequest, Result<AuthResponse>>
{

    public  async Task<Result<AuthResponse>> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var token = await dbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == request.Token, cancellationToken);
        if (token == null || !token.IsActive)
        {
            return Result.Invalid(new ValidationError("RefreshToken", "Invalid or expired refresh token."));
        }
        var user = await userManager.FindByIdAsync(token.UserId);
        if (user == null) return Result.Error("User not found");
        
        var roles = await userManager.GetRolesAsync(user);
        var newAccessToken = jwtTokenService.GenerateAccessToken(user, roles);
        var newRefreshToken = jwtTokenService.GenerateRefreshToken(request.IpAddress);
        
        token.Revoked = DateTime.UtcNow;
        token.ReplacedByToken = newRefreshToken.Token;
        
        newRefreshToken.UserId = user.Id;
        dbContext.RefreshTokens.Add(newRefreshToken);
        await dbContext.SaveChangesAsync();
        
        return Result.Success(new AuthResponse
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken.Token,
            ExpiresIn = 900
        });
    }
}