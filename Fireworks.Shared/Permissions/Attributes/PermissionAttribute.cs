using Fireworks.Shared.Permissions;

namespace Fireworks.Infrastructure.Permissions;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class PermissionAttribute(string name, string code, PermissionType type, int order, string? parentCode = null)
    : Attribute
{
    public string Name { get; set; } = name;
    public string Code { get; set; } = code;

    public PermissionType Type { get; set; } = type;
    public string? ParentCode { get; set; } = parentCode;
    public int Order { get; set; } = order;
}