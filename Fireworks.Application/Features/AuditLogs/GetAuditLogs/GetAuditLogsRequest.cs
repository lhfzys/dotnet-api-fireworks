using Ardalis.Result;
using Fireworks.Application.common.Models;
using Fireworks.Application.common.Requests;
using Fireworks.Application.Features.Permissions;
using MediatR;

namespace Fireworks.Application.Features.AuditLogs.GetAuditLogs;

public class GetAuditLogsRequest() : PaginationRequest,IRequest<Result<PaginatedResponse<AuditLogResponse>>>
{
    public string? Search { get; set; }
}