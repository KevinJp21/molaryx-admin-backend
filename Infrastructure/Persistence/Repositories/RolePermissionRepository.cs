using Domain.Contracts.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class RolePermissionRepository(AppDbContext dbContext) : BaseRepository<RolePermission, short>(dbContext), IRolePermissionRepository
    {
        public async Task<bool> RoleHasPermissionAsync(
            short IdUserRole,
            string permissionCode,
            CancellationToken cancellationToken = default)
        {
            return await DbSet
                .AsNoTracking()
                .AnyAsync(
                    rp =>
                        rp.IdUserRole == IdUserRole &&
                        rp.Permission.Code == permissionCode,
                    cancellationToken
                );
        }
    }
}