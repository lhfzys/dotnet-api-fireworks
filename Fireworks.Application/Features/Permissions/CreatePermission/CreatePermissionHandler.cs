using Ardalis.Result;
using Fireworks.Application.common;
using Fireworks.Application.common.Interfaces;
using Fireworks.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fireworks.Application.Features.Permissions.CreatePermission;

public class CreatePermissionHandler(IApplicationDbContext db) : IRequestHandler<CreatePermissionRequest, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreatePermissionRequest request, CancellationToken cancellationToken)
    {
        var exists = await db.Permissions.AnyAsync(p => p.Code == request.Code, cancellationToken);
        if (exists) return Result.Conflict("权限编码已存在");

        var entity = new Permission
        {
            Id  = Guid.NewGuid(),
            Name = request.Name,
            Code = request.Code,
            Type = request.Type,
            ParentId = request.ParentId,
            Url = request.Url,
            Icon = request.Icon,
            Order = request.Order,
            Description = request.Description
        };
        db.Permissions.Add(entity);
        await db.SaveChangesAsync(cancellationToken);
        return Result.Success(entity.Id);
    }
}