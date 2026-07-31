using Domain.Constants;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Services
{
    public class TenantSubscriptionService(
        IUnitOfWork unitOfWork,
        IPromotionService promotionService
    ) : ITenantSubscriptionService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IPromotionService _promotionService = promotionService;

        public async Task<TenantSubscription> CreateSubscriptionAsync(
            long idTenant,
            short idPlan,
            long? idPromotion,
            CancellationToken cancellationToken
        )
        {
            var tenant = await _unitOfWork.TenantRepository
                .GetByIdAsync(
                    idTenant,
                    cancellationToken
                );

            if (tenant is null)
            {
                throw new InvalidOperationException(
                    "El consultorio no existe."
                );
            }

            var plan = await _unitOfWork.PlanRepository
                .GetByIdAsync(
                    idPlan,
                    cancellationToken
                );

            if (plan is null || !plan.IsActive)
            {
                throw new InvalidOperationException(
                    "El plan seleccionado no está disponible."
                );
            }

            var activeSubscription = await _unitOfWork
                .TenantSubscriptionRepository
                .GetActiveSubscriptionAsync(
                    idTenant,
                    cancellationToken
                );

            if (activeSubscription is not null)
            {
                throw new InvalidOperationException(
                    "El consultorio ya tiene una suscripción activa."
                );
            }

            decimal price;

            if (idPromotion != null)
            {
                var promotion = await _promotionService
                    .ValidatePromotionAsync(
                        idPromotion.Value,
                        idPlan,
                        cancellationToken
                    );

                price = _promotionService.GetPromotionPrice(
                    promotion,
                    idPlan
                );
            }
            else
            {
                price = plan.Price
                    ?? throw new InvalidOperationException(
                        "El plan seleccionado no está disponible para contratación directa."
                    );
            }

            var subscription = new TenantSubscription
            {
                IdTenant = idTenant,
                IdPlan = idPlan,
                IdPromotion = idPromotion,

                IdTenantSubscriptionStatus =
                    (short)TenantSubscriptionStatusEnum.PENDING,

                Price = price,

                MaxProfessionals = plan.MaxProfessionals,
                MaxAssistants = plan.MaxAssistants,
                MaxPatients = plan.MaxPatients,

                StartsAt = null,
                EndsAt = null
            };

            await _unitOfWork.TenantSubscriptionRepository
                .AddAsync(
                    subscription,
                    cancellationToken
                );

            return subscription;
        }

        public async Task<TenantSubscription> CreateCustomSubscriptionAsync(
            long idTenant,
            decimal price,
            short? maxProfessionals,
            short? maxAssistants,
            int? maxPatients,
            CancellationToken cancellationToken
        )
        {
            var tenant = await _unitOfWork.TenantRepository
                .GetByIdAsync(
                    idTenant,
                    cancellationToken
                );

            if (tenant is null)
            {
                throw new InvalidOperationException(
                    "El consultorio no existe."
                );
            }

            var plan = await _unitOfWork.PlanRepository
                .GetByIdAsync(
                    (short)PlanEnum.BUSINESS,
                    cancellationToken
                );

            if (plan is null || !plan.IsActive)
            {
                throw new InvalidOperationException(
                    "El plan seleccionado no está disponible."
                );
            }

            if (plan.Code != PlanCodes.BUSINESS)
            {
                throw new InvalidOperationException(
                    "El plan seleccionado no admite una configuración personalizada."
                );
            }

            var activeSubscription = await _unitOfWork
                .TenantSubscriptionRepository
                .GetActiveSubscriptionAsync(
                    idTenant,
                    cancellationToken
                );

            if (activeSubscription is not null)
            {
                throw new InvalidOperationException(
                    "El consultorio ya tiene una suscripción activa."
                );
            }

            if (price <= 0)
            {
                throw new InvalidOperationException(
                    "El precio de la suscripción debe ser mayor que cero."
                );
            }

            var subscription = new TenantSubscription
            {
                IdTenant = idTenant,
                IdPlan = (short)PlanEnum.BUSINESS,
                IdTenantSubscriptionStatus =
                    (short)TenantSubscriptionStatusEnum.PENDING,

                Price = price,
                MaxProfessionals = maxProfessionals,
                MaxAssistants = maxAssistants,
                MaxPatients = maxPatients,

                StartsAt = null,
                EndsAt = null
            };

            await _unitOfWork.TenantSubscriptionRepository
                .AddAsync(
                    subscription,
                    cancellationToken
                );

            return subscription;
        }

        public async Task<TenantSubscription> ActivateSubscriptionAsync(
            long idTenantSubscription,
            CancellationToken cancellationToken)
        {
            var subscription = await _unitOfWork.TenantSubscriptionRepository
                .GetByIdWithPromotionAsync(idTenantSubscription, cancellationToken);

            if (subscription is null)
            {
                throw new InvalidOperationException("La subscription no existe.");
            }

            if (subscription.IdTenantSubscriptionStatus != (short)TenantSubscriptionStatusEnum.PENDING)
            {
                throw new InvalidOperationException("La suscripción no se encuentra pendiente de activación.");
            }

            var startsAt = DateTime.UtcNow;

            subscription.IdTenantSubscriptionStatus = (short)TenantSubscriptionStatusEnum.ACTIVE;

            subscription.StartsAt = startsAt;

            if (subscription.IdPromotion.HasValue)
            {
                if (subscription.Promotion is null)
                {
                    throw new InvalidOperationException("No se pudo obtener la promoción asociada a la suscripción.");
                }

                subscription.PromotionEndsAt = startsAt.AddMonths(subscription.Promotion.DurationMonths);
            }

            await _unitOfWork.TenantSubscriptionRepository
                .UpdateAsync(subscription, cancellationToken);

            return subscription;
        }

        public async Task<int> UpdateExpiredPromotionsAsync(CancellationToken cancellationToken)
        {
            var expiredSubscriptions = await _unitOfWork.TenantSubscriptionRepository
                .GetSubscriptionsWithExpiredPromotionsAsync(
                    DateTime.UtcNow,
                    cancellationToken
                );

            var updatedCount = 0;

            foreach (var subscription in expiredSubscriptions)
            {
                if (subscription.Plan.Price is null)
                {
                    continue;
                }

                subscription.Price = subscription.Plan.Price.Value;

                subscription.IdPromotion = null;
                subscription.PromotionEndsAt = null;

                await _unitOfWork.TenantSubscriptionRepository.UpdateAsync(subscription, cancellationToken);

                updatedCount++;
            }

            if (updatedCount > 0)
            {
                await _unitOfWork.SaveChangeAsync(cancellationToken);
            }

            return updatedCount;
        }
    }
}