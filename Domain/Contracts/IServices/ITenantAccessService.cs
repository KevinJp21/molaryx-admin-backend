using Domain.Entities;

namespace Domain.Contracts.IServices
{
    public record TenantAccessContext(
        long IdTenant,
        Tenant Tenant,
        TenantSubscription Subscription
    );

    public interface ITenantAccessService
    {
        Task<TenantAccessContext> RequireActiveAsync(CancellationToken cancellationToken = default);
    }
}
