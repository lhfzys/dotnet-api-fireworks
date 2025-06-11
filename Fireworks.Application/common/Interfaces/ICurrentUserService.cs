namespace Fireworks.Application.common.Interfaces;

public interface ICurrentUserService
{
    Guid Id { get; }
    string UserName { get; }
    List<string> Roles { get; }
    List<string> Permissions { get; }
}