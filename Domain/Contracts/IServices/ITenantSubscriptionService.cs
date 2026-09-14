using Application.Features.Platform.Tenant.Command.UpdateTenant;
using Domain.Entities;

namespace Domain.Contracts.IServices
{
    public interface ITenantSubscriptionService
    {
        Task<TenantSubscription> CreateSubscriptionAsync(long idTenant, short idPlan, long? idPromotion, CancellationToken cancellationToken);

        Task<TenantSubscription> CreateCustomSubscriptionAsync(
            long idTenant,
            decimal price,
            short? maxProfessionals,
            short? maxAssistants,
            int? maxPatients,
            CancellationToken cancellationToken
        );

        Task<TenantSubscription> ActivateSubscriptionAsync(long idTenantSubscription, CancellationToken cancellationToken);

        Task UpdateSubscriptionAsync(
            long idTenant,
            UpdateTenantSubscriptionRequest request,
            CancellationToken cancellationToken);

        Task<int> UpdateExpiredPromotionsAsync(CancellationToken cancellationToken);

        Task<int> UpdateExpiredSubscriptionsAsync(CancellationToken cancellationToken);
    }
}
