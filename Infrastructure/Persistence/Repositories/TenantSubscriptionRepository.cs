using Domain.Contracts.IRepositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class TenantSubscriptionRepository(AppDbContext dbContext)
        : BaseRepository<TenantSubscription, long>(dbContext), ITenantSubscriptionRepository
    {
    }
}
