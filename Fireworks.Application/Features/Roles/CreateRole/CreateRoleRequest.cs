using Ardalis.Result;
using MediatR;

namespace Fireworks.Application.Features.Roles;

public record CreateRoleRequest(string RoleName):IRequest<Result<Guid>>;