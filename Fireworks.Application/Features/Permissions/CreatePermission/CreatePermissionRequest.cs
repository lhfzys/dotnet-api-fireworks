using Ardalis.Result;
using Fireworks.Domain.Constants;
using MediatR;

namespace Fireworks.Application.Features.Permissions.CreatePermission;

public record CreatePermissionRequest(
    string Name,
    string Code,
    PermissionType Type,
    Guid? ParentId,
    string? Url,
    string? Icon,
    int Order,
    string Description) : IRequest<Result<Guid>>;