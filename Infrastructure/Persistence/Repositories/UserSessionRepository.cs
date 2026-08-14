using Domain.Contracts.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class UserSessionRepository(AppDbContext dbContext)
        : BaseRepository<UserSession, long>(dbContext), IUserSessionRepository
    {
        public async Task<bool> RevokeSessionAsync(
            long idUser,
            string refreshTokenHash,
            DateTime currentDate,
            CancellationToken cancellationToken)
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
                        .SetProperty(us => us.RevokedAt, currentDate)
                        .SetProperty(us => us.UpdatedAt, currentDate),
                    cancellationToken
                );

            return affectedRows == 1;
        }
    }
}
