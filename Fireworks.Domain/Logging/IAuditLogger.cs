namespace Fireworks.Domain.Logging;

public interface IAuditLogger
{
    Task LogAsync(string action, string requestData, string responseData, bool success, Guid? userId = null,
        string? userName = null);
}