using Ardalis.Result;
using Fireworks.Application.common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fireworks.Application.Features.Permissions.DeletePermission;

public class DeletePermissionHandler(IApplicationDbContext db) : IRequestHandler<DeletePermissionRequest, Result>
{
    public async Task<Result> Handle(DeletePermissionRequest request, CancellationToken cancellationToken)
    {
        var entity = await db.Permissions
            .Include(p => p.Children)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (entity == null)
            return Result.NotFound("权限不存在");

        if (entity.Children.Any())
            return Result.Conflict("该权限下有子权限，无法删除");

        db.Permissions.Remove(entity);
        await db.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
