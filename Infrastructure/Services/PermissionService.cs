using Domain.Contracts.IRepositories;
using Domain.Contracts.IServices;

namespace Infrastructure.Services
{
    public class PermissionService(IRoleHasPermissionRepository roleHasPermissionRepository) : IPermissionService
    {
        private readonly IRoleHasPermissionRepository _roleHasPermissionRepository = roleHasPermissionRepository;

        public async Task<bool> RoleHasPermissionAsync(
            short idUserRole,
            string permissionCode,
            CancellationToken cancellationToken = default
        )
        {
            return await _roleHasPermissionRepository.RoleHasPermissionAsync(
                idUserRole,
                permissionCode,
                cancellationToken
            );
        }
    }
}