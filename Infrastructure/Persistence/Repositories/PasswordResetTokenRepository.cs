using Domain.Contracts.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class PasswordResetTokenRepository(AppDbContext context) : BaseRepository<PasswordResetToken, long>(context), IPasswordResetTokenRepository
    {
        public async Task<PasswordResetToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            return await DbSet
            .FirstOrDefaultAsync
            (
                p => p.Token == token
                && p.ExpiresAt > DateTime.UtcNow
                && p.UsedAt == null,
                cancellationToken
            );
        }

        public async Task<List<PasswordResetToken>> GetActiveTokensByUserIdAsync(long userId, CancellationToken cancellationToken = default)
        {
            return await DbSet
                .Where(
                    p => p.IdUser == userId
                    && p.ExpiresAt > DateTime.UtcNow
                    && p.UsedAt == null
                )
                .ToListAsync(cancellationToken);
        }
    }
}