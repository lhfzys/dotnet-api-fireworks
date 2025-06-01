using Ardalis.Result;
using Fireworks.Application.common.Models;
using Fireworks.Application.common.Requests;
using Fireworks.Application.Features.Users;
using MediatR;

namespace Fireworks.Application.Features.Permissions.GetAllPermissions;

public class GetPermissionsRequest :PaginationRequest,  IRequest<Result<PaginatedResponse<PermissionResponse>>>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Search { get; set; } // 可模糊匹配 Name / Code
}
