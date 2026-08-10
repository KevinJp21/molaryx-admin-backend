using Domain.Contracts.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository(AppDbContext dbContext) : BaseRepository<User, long>(dbContext), IUserRepository
    {
        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await DbSet
                .Include(u => u.UserRole)
                .Include(u => u.UserStatus)
                .Include(u => u.Tenant)
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        public override async Task<User?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await DbSet
                .Include(u => u.UserRole)
                .Include(u => u.UserStatus)
                .Include(u => u.Tenant)
                .FirstOrDefaultAsync(
                    u => u.IdUser == id,
                    cancellationToken
            );
        }

        public async Task<bool> ExistsByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            return await DbSet
                .AnyAsync
                (
                    u => u.Username == username,
                    cancellationToken
                );
        }

        public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await DbSet
                .AnyAsync
                (
                    u => u.Email == email,
                    cancellationToken
                );
        }

        public async Task<bool> ExistsByIdentificationNumberAsync(string identificationNumber, CancellationToken cancellationToken)
        {
            return await DbSet
                .AnyAsync
                (
                    u => u.IdentificationNumber == identificationNumber,
                    cancellationToken
                );
        }

        public async Task<bool> ExistsByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken)
        {
            return await DbSet
                .AnyAsync
                (
                    u => u.PhoneNumber == phoneNumber,
                    cancellationToken
                );
        }

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