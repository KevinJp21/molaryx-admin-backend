using Domain.Contracts.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class UserSessionRepository(AppDbContext dbContext) : BaseRepository<UserSession, long>(dbContext), IUserSessionRepository
    {
        public async Task<UserSession?> GetByRefreshTokenHashAsync(string refreshTokenHash, CancellationToken cancellationToken = default)
        {
            return await DbSet
                .FirstOrDefaultAsync(us => us.RefreshTokenHash == refreshTokenHash, cancellationToken);
        }

        public async Task<IEnumerable<UserSession>> GetActiveSessionsByUserIdAsync(long idUser, CancellationToken cancellationToken = default)
        {
            return await DbSet
                .Where(us =>
                    us.IdUser == idUser &&
                    us.RevokedAt == null &&
                    us.ExpiresAt > DateTime.UtcNow
                )
                .ToListAsync(cancellationToken);
        }
    }
}