using Ardalis.Result;
using Fireworks.Application.common;
using Fireworks.Application.common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fireworks.Application.Features.Auth.logout;

public class LogoutHandler(IApplicationDbContext context):IRequestHandler<LogoutRequest, Result>
{
    public async Task<Result> Handle(LogoutRequest request, CancellationToken cancellationToken)
    {
        var refreshToken = await context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken, cancellationToken);
        
        if (refreshToken is null || !refreshToken.IsActive)
        {
            return Result.Invalid(new ValidationError(nameof(request.RefreshToken), "无效的 RefreshToken"));
        }
        
        refreshToken.Revoked = DateTime.UtcNow;
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}