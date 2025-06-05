using Fireworks.Domain.Entities;

namespace Fireworks.Domain.Constants;

public interface IAuditLogService
{
    Task LogAsync(AuditLog log, CancellationToken cancellationToken = default); 
}