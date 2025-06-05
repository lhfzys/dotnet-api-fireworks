using Fireworks.Application.common.extensions;
using Fireworks.Application.common.Handlers;
using Fireworks.Application.common.Interfaces;
using Fireworks.Domain.Entities;

namespace Fireworks.Application.Features.Permissions.GetAllPermissions;

public class GetPermissionsHandler(IApplicationDbContext context)
    : PaginationQueryHandler<GetPermissionsRequest, Permission, PermissionResponse>(context)
{
    protected override IQueryable<Permission> BuildQuery(GetPermissionsRequest request)
    {
        var query = context.Permissions.AsQueryable();

        query = query.WhereIf(!string.IsNullOrWhiteSpace(request.Name),
                p => request.Name != null && p.Name.Contains(request.Name))
            .WhereIf(!string.IsNullOrWhiteSpace(request.Code),
                p => request.Code != null && p.Code.Contains(request.Code, StringComparison.CurrentCultureIgnoreCase));

        return query.OrderBy(u => u.Order);
    }
}