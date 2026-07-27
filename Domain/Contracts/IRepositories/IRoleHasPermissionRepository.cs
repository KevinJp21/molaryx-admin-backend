using Domain.Common;
using Domain.Entities;

namespace Domain.Contracts.IRepositories
{
    public interface IRoleHasPermissionRepository : IBaseRepository<RolePermission, short>
    {
        Task<bool> RoleHasPermissionAsync(
            short idUserRole,
            string PermissionCode,
            CancellationToken cancellationToken = default
        );
    }
}