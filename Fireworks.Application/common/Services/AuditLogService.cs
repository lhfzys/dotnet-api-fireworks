using Fireworks.Application.common.Interfaces;
using Fireworks.Domain.Constants;
using Fireworks.Domain.Entities;

namespace Fireworks.Application.common.Services;

public class AuditLogService(IApplicationDbContext dbContext):IAuditLogService
{
    public async Task LogAsync(AuditLog log, CancellationToken cancellationToken = default)
    {
         dbContext.AuditLogs.Add(log);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}