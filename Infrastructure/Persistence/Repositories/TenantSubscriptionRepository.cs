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
    }
}