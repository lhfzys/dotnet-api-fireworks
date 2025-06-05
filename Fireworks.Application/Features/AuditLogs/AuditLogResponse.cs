namespace Fireworks.Application.Features.AuditLogs;

public class AuditLogResponse
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string HttpMethod { get; set; } = string.Empty;
    public int StatusCode { get; set; }
    public long ExecutionDurationMs { get; set; }
    public DateTime Timestamp { get; set; }
}