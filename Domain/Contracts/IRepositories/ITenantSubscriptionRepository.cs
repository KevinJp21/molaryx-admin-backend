using Domain.Common;
using Domain.Entities;

namespace Domain.Contracts.IRepositories
{
    public interface ITenantSubscriptionRepository : IBaseRepository<TenantSubscription, long>
    {
    }
}
