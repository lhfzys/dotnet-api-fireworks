using Fireworks.Application.common;
using Fireworks.Application.common.Handlers;
using Fireworks.Domain.Entities;

namespace Fireworks.Application.Features.Permissions.GetAllPermissions;

public class GetPermissionsHandler(IApplicationDbContext context)
    : PaginationQueryHandler<GetPermissionsRequest, Permission, PermissionResponse>(context)
{
    protected override IQueryable<Permission> BuildQuery(GetPermissionsRequest request)
    {
        var query = context.Permissions.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            query = query.Where(p =>
                p.Name!.Contains(request.Search) ||
                p.Code.Contains(request.Search, StringComparison.CurrentCultureIgnoreCase));
        }

        return query.OrderBy(u => u.Order);
    }
}