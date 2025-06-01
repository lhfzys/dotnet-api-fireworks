using Ardalis.Result;
using Fireworks.Domain.Constants;
using MediatR;

namespace Fireworks.Application.Features.Permissions.UpdatePermission;

public record UpdatePermissionRequest(
    Guid Id,
    string? Name,
    string? Code,
    PermissionType? Type,
    Guid? ParentId,
    string? Url,
    string? Icon,
    int? Order,
    string? Description,
    bool? IsEnabled
) : IRequest<Result>;
