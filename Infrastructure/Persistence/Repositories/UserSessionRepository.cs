using Domain.Common;
using Domain.Contracts.IRepositories;
using Domain.Entities;
using Domain.Specifications;
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

        public async Task<List<UserSession>> GetActiveSessionsByUserIdAsync(
            long idUser,
            CancellationToken cancellationToken = default)
        {
            return await DbSet
                .Where(us =>
                    us.IdUser == idUser &&
                    us.RevokedAt == null &&
                    us.ExpiresAt > DateTime.UtcNow
                )
                .ToListAsync(cancellationToken);
        }

        public async Task<(int totalItems, List<UserSession> data)> GetAllSessionsByUserIdAsync(
            ISpecification<UserSession> spec,
            int page,
            int size,
            CancellationToken cancellationToken = default)
        {
            var query = DbSet
                .AsNoTracking()
                .Where(spec.Criteria!);

            var totalItems = await query.CountAsync(
                cancellationToken
            );

            var sessions = await query
                .OrderByDescending(us => us.CreatedAt)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(cancellationToken);

            return (
                totalItems,
                sessions
            );
        }

        public async Task<bool> RevokeSessionAsync(long idUser, string refreshTokenHash, DateTime currentDate, CancellationToken cancellationToken)
        {
            var affectedRows = await DbSet
                .Where(us =>
                    us.IdUser == idUser &&
                    us.RefreshTokenHash == refreshTokenHash &&
                    us.RevokedAt == null &&
                    us.ExpiresAt > currentDate
                )
                .ExecuteUpdateAsync(
                    setters => setters
                        .SetProperty(
                            us => us.RevokedAt,
                            currentDate
                        )
                        .SetProperty(
                            us => us.UpdatedAt,
                            currentDate
                        ),
                    cancellationToken
                );

            return affectedRows == 1;
        }
    }
}