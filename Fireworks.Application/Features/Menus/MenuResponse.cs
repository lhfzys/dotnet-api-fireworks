using System.Collections;

namespace Fireworks.Application.Features.Menus;

public record MenuResponse(
    Guid Id,
    string Name,
    string? Icon,
    string? Url,
    int Order,
    List<MenuResponse> Children);