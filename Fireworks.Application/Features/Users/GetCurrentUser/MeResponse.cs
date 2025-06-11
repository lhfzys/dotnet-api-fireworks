namespace Fireworks.Application.Features.Users.GetCurrentUser;

public record MeResponse
{
   public Guid Id { get; init; }
   public string UserName { get; init; } = null!;
   public string[] Roles { get; init; } = [];
   public string[] Permissions { get; init; } = [];
};