using Domain.Contracts.IRepositories;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class TenantSubscriptionRepository(AppDbContext dbContext) : BaseRepository<TenantSubscription, long>(dbContext), ITenantSubscriptionRepository
    {
        public async Task<TenantSubscription?> GetActiveSubscriptionAsync(long idTenant, CancellationToken cancellationToken = default)
        {
            return await DbSet
                .Include(ts => ts.TenantSubscriptionStatus)
                .FirstOrDefaultAsync(
                    ts => ts.IdTenant == idTenant &&
                    ts.IdTenantSubscriptionStatus == (short)TenantSubscriptionStatusEnum.ACTIVE,
                    cancellationToken
                );
        }

        public async Task<TenantSubscription?> GetByIdWithPromotionAsync(
            long idTenantSubscription,
            CancellationToken cancellationToken
        )
        {
            return await DbSet
                .Include(ts => ts.Promotion)
                .FirstOrDefaultAsync(
                    ts => ts.IdTenantSubscription == idTenantSubscription,
                    cancellationToken
                );
        }

        public async Task<List<TenantSubscription>> GetSubscriptionsWithExpiredPromotionsAsync(
            DateTime currentDate,
            CancellationToken cancellationToken)
        {
            return await DbSet
                .Include(ts => ts.Promotion)
                .Include(ts => ts.Plan)
                .Where(ts =>
                    ts.IdTenantSubscriptionStatus == (short)TenantSubscriptionStatusEnum.ACTIVE
                    && ts.IdPromotion.HasValue
                    && ts.PromotionEndsAt.HasValue
                    && ts.PromotionEndsAt.Value <= currentDate
                ).ToListAsync(cancellationToken);
        }
    }
}