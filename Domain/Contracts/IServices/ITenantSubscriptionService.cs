using Domain.Entities;

namespace Domain.Contracts.IServices
{
    public interface ITenantSubscriptionService
    {
        Task<TenantSubscription> CreateSubscriptionAsync(long idTenant, short idPlan, CancellationToken cancellationToken);

        Task<TenantSubscription> CreateCustomSubscriptionAsync(
            long idTenant,
            short idPlan,
            decimal price,
            short? maxProfessionals,
            short? maxAssistants,
            int? maxPatients,
            CancellationToken cancellationToken
        );
    }
}