using Fireworks.Application.common.Handlers;
using Fireworks.Application.common.Interfaces;
using Fireworks.Domain.Entities;

namespace Fireworks.Application.Features.AuditLogs.GetAuditLogs;

public class GetAuditLogsHandler(IApplicationDbContext context)
    : PaginationQueryHandler<GetAuditLogsRequest, AuditLog, AuditLogResponse>(context)
{
    protected override IQueryable<AuditLog> BuildQuery(GetAuditLogsRequest request)
    {
        var query = context.AuditLogs.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            query = query.Where(p =>
                p.UserName!.Contains(request.Search) ||
                p.Url.Contains(request.Search, StringComparison.CurrentCultureIgnoreCase));
        }

        return query.OrderBy(p => p.Timestamp);
    }
}