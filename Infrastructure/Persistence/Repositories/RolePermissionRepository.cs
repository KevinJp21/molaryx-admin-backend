using Domain.Contracts.IRepositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class RolePermissionRepository(AppDbContext dbContext)
        : BaseRepository<RolePermission, short>(dbContext), IRolePermissionRepository
    {
    }
}
