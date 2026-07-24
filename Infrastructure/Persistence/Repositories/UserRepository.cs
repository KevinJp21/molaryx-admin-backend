using Domain.Contracts.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository(AppDbContext dbContext) : BaseRepository<User, long>(dbContext), IUserRepository
    {
        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await DbSet.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        public override async Task<User?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await DbSet
                .Include(u => u.UserRole)
                .FirstOrDefaultAsync(
                    u => u.IdUser == id,
                    cancellationToken
            );
        }
    }
}