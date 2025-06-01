using Ardalis.Result;
using MediatR;

namespace Fireworks.Application.Features.Permissions.DeletePermission;

public record DeletePermissionRequest(Guid Id) : IRequest<Result>;
