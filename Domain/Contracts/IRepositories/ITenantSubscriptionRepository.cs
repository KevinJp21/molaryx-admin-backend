using Domain.Common;
using Domain.Entities;

namespace Domain.Contracts.IRepositories
{
    public interface ITenantSubscriptionRepository : IBaseRepository<TenantSubscription, long>
    {
        Task<TenantSubscription?> GetActiveSubscriptionAsync(long IdTenant, CancellationToken cancellationToken);
    }
}