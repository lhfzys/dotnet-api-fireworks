namespace Fireworks.Application.Features.Users;

public class UserResponse
{
    public Guid Id { get; set; }
    public string? UserName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? ImageUrl { get; set; }
}