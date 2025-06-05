using Fireworks.Shared.Permissions;

namespace Fireworks.Application.Features.Permissions;

public class PermissionResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public PermissionType Type { get; set; }
    public Guid? ParentId { get; set; }
    public string? Url { get; set; }
    public string? Icon { get; set; }
    public int Order { get; set; }
    public bool IsEnabled { get; set; }
    public string Description { get; set; } = string.Empty;
}