using Domain.Common;
using Domain.Contracts.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository(AppDbContext dbContext) : BaseRepository<User, long>(dbContext), IUserRepository
    {
        public async Task<List<Permission>> GetPermissionsByUserIdAsync(long userId, CancellationToken cancellationToken = default)
        {
            return await DbSet
                .AsNoTracking()
                .Where(u => u.IdUser == userId)
                .SelectMany(u => u.UserRole.RolePermissions)
                .Select(rp => new Permission
                {
                    Code = rp.Permission.Code,
                    Module = new Module
                    {
                        Code = rp.Permission.Module.Code
                    }
                })
                .ToListAsync(cancellationToken);
        }
    }
}
