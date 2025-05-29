namespace Fireworks.Application.Features.Auth;

public class AuthResponse
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    
    public DateTime ExpiresAt { get; set; }
}