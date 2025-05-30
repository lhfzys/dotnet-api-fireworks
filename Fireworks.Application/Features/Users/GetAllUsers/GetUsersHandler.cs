using Fireworks.Application.common;
using Fireworks.Application.common.Handlers;
using Fireworks.Domain.Identity.Entities;


namespace Fireworks.Application.Features.Users.GetAllUsers;

public class GetUsersHandler(IApplicationDbContext context)
    : PaginationQueryHandler<GetUsersRequest, ApplicationUser, UserResponse>(context)
{
    protected override IQueryable<ApplicationUser> BuildQuery(GetUsersRequest request)
    {
        var query = context.Users.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            query = query.Where(u =>
                u.UserName!.Contains(request.Search) ||
                u.Email!.Contains(request.Search));
        }

        return query.OrderBy(u => u.UserName);
    }
}