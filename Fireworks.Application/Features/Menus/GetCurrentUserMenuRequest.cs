using Ardalis.Result;
using MediatR;

namespace Fireworks.Application.Features.Menus;

public record GetCurrentUserMenuRequest():IRequest<Result<List<MenuResponse>>>;