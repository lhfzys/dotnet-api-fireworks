using Ardalis.Result;
using Fireworks.Application.common.Models;
using Fireworks.Application.common.Requests;
using MediatR;

namespace Fireworks.Application.Features.Permissions.GetAllPermissions;

public class GetPermissionsRequest : PaginationRequest, IRequest<Result<PaginatedResponse<PermissionResponse>>>
{
    public string? Name { get; set; }
    public string? Code { get; set; }
}