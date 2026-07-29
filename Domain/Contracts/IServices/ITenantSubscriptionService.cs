using Domain.Entities;

namespace Domain.Contracts.IServices
{
    public interface ITenantSubscriptionService
    {
        Task<TenantSubscription> CreateSubscriptionAsync(long idTenant, short idPlan, string? promotionCode, CancellationToken cancellationToken);

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