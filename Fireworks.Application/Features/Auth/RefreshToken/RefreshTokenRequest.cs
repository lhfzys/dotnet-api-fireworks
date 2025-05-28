using Ardalis.Result;
using MediatR;

namespace Fireworks.Application.Features.Auth.RefreshToken;

public class RefreshTokenRequest: IRequest<Result<AuthResponse>>
{
    public string Token { get; set; } = null!;
    public string IpAddress { get; set; } = null!;
}