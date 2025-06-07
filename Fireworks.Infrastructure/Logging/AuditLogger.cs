using Fireworks.Application.common.Interfaces;
using Fireworks.Domain.Entities;
using Fireworks.Domain.Logging;
using Microsoft.AspNetCore.Http;

namespace Fireworks.Infrastructure.Logging;

public class AuditLogger(
    IApplicationDbContext db,
    ICurrentUserService currentUser,
    IHttpContextAccessor httpContextAccessor) : IAuditLogger
{
    public async Task LogAsync(string action, string requestData, string responseData, bool success,
        Guid? userId = null,
        string? userName = null)
    {
        var context = httpContextAccessor.HttpContext;
        var audit = new AuditLog
        {
            UserId = userId ?? currentUser.Id,
            UserName = userName ?? currentUser.UserName ?? "Unknown",
            HttpMethod = context?.Request.Method ?? "N/A",
            Url = context?.Request.Path.Value ?? "N/A",
            Action = action,
            IpAddress = context?.Connection.RemoteIpAddress?.ToString() ?? "N/A",
            UserAgent = context?.Request.Headers["User-Agent"].ToString() ?? "N/A",
            RequestData = requestData,
            ResponseData = responseData,
            StatusCode = context?.Response.StatusCode ?? 0,
            ExecutionDurationMs = 0,
            Timestamp = DateTime.UtcNow
        };
        db.AuditLogs.Add(audit);
        await db.SaveChangesAsync();
    }
}