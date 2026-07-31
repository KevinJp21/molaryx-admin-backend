using Domain.Contracts.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class PromotionRepository(AppDbContext dbContext) : BaseRepository<Promotion, long>(dbContext), IPromotionRepository
    {
        public async Task<Promotion?> GetActivePromotionAsync(long idPromotion, CancellationToken cancellationToken)
        {
            return await DbSet
                .Include(p => p.PromotionPlans)
                .FirstOrDefaultAsync(
                    p =>
                        p.IdPromotion == idPromotion &&
                        p.IsActive,
                        cancellationToken
                );
        }

        public async Task<Promotion?> GetAvailablePromotionAsync(long idPromotion, CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;

            return await DbSet
                .Include(p => p.PromotionPlans)
                .FirstOrDefaultAsync(
                    p =>
                        p.IdPromotion == idPromotion &&
                        p.IsActive &&
                        p.StartsAt <= now &&
                        (
                            p.EndsAt == null ||
                            p.EndsAt > now
                        ),
                        cancellationToken
                );
        }
    }
}