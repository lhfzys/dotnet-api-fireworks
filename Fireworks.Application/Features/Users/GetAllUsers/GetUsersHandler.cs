using Fireworks.Application.common;
using Fireworks.Application.common.extensions;
using Fireworks.Application.common.Handlers;
using Fireworks.Application.common.Interfaces;
using Fireworks.Domain.Identity.Entities;


namespace Fireworks.Application.Features.Users.GetAllUsers;

public class GetUsersHandler(IApplicationDbContext context)
    : PaginationQueryHandler<GetUsersRequest, ApplicationUser, UserResponse>(context)
{
    protected override IQueryable<ApplicationUser> BuildQuery(GetUsersRequest request)
    {
        var query = context.Users.AsQueryable();

        query = query.WhereIf(!string.IsNullOrWhiteSpace(request.Username),
            u => request.Username != null && u.UserName!.Contains(request.Username));

        return query.OrderBy(u => u.UserName);
    }
}