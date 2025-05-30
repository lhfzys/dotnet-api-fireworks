using Fireworks.Domain.Identity.Entities;

namespace Fireworks.Application.common;

public interface IPermissionService
{
    Task<List<string>> GetPermissionsForUserAsync(ApplicationUser user, CancellationToken cancellationToken = default);
}