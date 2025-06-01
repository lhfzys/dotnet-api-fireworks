using Ardalis.Result;
using Fireworks.Application.common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fireworks.Application.Features.Permissions.UpdatePermission;

public class UpdatePermissionHandler(IApplicationDbContext db) : IRequestHandler<UpdatePermissionRequest, Result>
{
    
    public async Task<Result> Handle(UpdatePermissionRequest request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Error occurred while saving changes: ");
        var entity = await db.Permissions.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        if (entity == null)
            return Result.NotFound("权限不存在");

        if (!string.IsNullOrWhiteSpace(request.Code) &&
            await db.Permissions.AnyAsync(x => x.Code == request.Code && x.Id != request.Id, cancellationToken))
        {
            return Result.Conflict("权限编码已存在");
        }

        if (request.Name is not null)
            entity.Name = request.Name;
        
        if (request.Code is not null)
            entity.Code = request.Code;
        
        if (request.Type.HasValue)
            entity.Type = request.Type.Value;
        
        if (request.ParentId.HasValue)
            entity.ParentId = request.ParentId;
        
        if (request.Url is not null)
            entity.Url = request.Url;
        
        if (request.Icon is not null)
            entity.Icon = request.Icon;
        
        if (request.Order.HasValue)
            entity.Order = request.Order.Value;
        
        if (request.Description is not null)
            entity.Description = request.Description;
        
        if (request.IsEnabled.HasValue)
            entity.IsEnabled = request.IsEnabled.Value;

        try
        {
            await db.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (DbUpdateException ex)
        {
            return Result.Error(ex.InnerException?.Message ?? ex.Message);
        }
    }
}