using Fireworks.Domain.Identity.Entities;

namespace Fireworks.Application.common.Interfaces;

public interface IPermissionService
{
    Task<List<string>> GetPermissionsForUserAsync(ApplicationUser user, CancellationToken cancellationToken = default);
}