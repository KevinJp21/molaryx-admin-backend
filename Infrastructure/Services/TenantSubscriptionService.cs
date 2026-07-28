using Domain.Constants;
using Domain.Contracts.IRepositories;
using Domain.Contracts.IServices;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Constants;

namespace Infrastructure.Services
{
    public class TenantSubscriptionService(
        ITenantSubscriptionRepository tenantSubscriptionRepository,
        ITenantRepository tenantRepository,
        IPlanRepository planRepository
    ) : ITenantSubscriptionService
    {
        private readonly ITenantSubscriptionRepository _tenantSubscriptionRepository = tenantSubscriptionRepository;

        private readonly ITenantRepository _tenantRepository = tenantRepository;

        private readonly IPlanRepository _planRepository = planRepository;

        public async Task<TenantSubscription> CreateSubscriptionAsync(long idTenant, short idPlan, CancellationToken cancellationToken)
        {
            var plan = await _planRepository.GetByIdAsync(idPlan, cancellationToken);

            if (plan is null || !plan.IsActive)
            {
                throw new InvalidOperationException("El plan seleccionado no está disponible.");
            }

            var activeSubscription = await _tenantSubscriptionRepository.GetActiveSubscriptionAsync(idTenant, cancellationToken);

            if (activeSubscription is not null)
            {
                throw new InvalidOperationException("El consultorio ya tiene una suscripción activa.");
            }

            var subscription = new TenantSubscription
            {
                IdTenant = idTenant,
                IdPlan = idPlan,
                IdTenantSubscriptionStatus = (short)TenantSubscriptionStatusEnum.PENDING,
                Price = plan.Price ?? throw new InvalidOperationException("El plan seleccionado no está disponible para contratación directa."),
                MaxProfessionals = plan.MaxProfessionals,
                MaxAssistants = plan.MaxAssistants,
                MaxPatients = plan.MaxPatients,
                StartsAt = null,
                EndsAt = null,
                CreatedAt = SeedConstants.SeedDate
            };

            await _tenantSubscriptionRepository.AddAsync(subscription, cancellationToken);

            return subscription;
        }

        public async Task<TenantSubscription> CreateCustomSubscriptionAsync(
            long idTenant,
            short idPlan,
            decimal price,
            short? maxProfessionals,
            short? maxAssistants,
            int? maxPatients,
            CancellationToken cancellationToken)
        {
            var tenant = await _tenantRepository.GetByIdAsync(idTenant, cancellationToken);

            if (tenant is null)
            {
                throw new InvalidOperationException("El consultorio no existe.");
            }

            var plan = await _planRepository.GetByIdAsync(idPlan);

            if (plan is null || !plan.IsActive)
            {
                throw new InvalidOperationException("El plan seleccionado no está disponible.");
            }

            if (plan.Code != PlanCodes.BUSINESS)
            {
                throw new InvalidOperationException(
                    "El plan seleccionado no admite una configuración personalizada."
                );
            }

            var activeSubscription = await _tenantSubscriptionRepository.GetActiveSubscriptionAsync(
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
                IdPlan = idPlan,
                IdTenantSubscriptionStatus = (short)TenantSubscriptionStatusEnum.PENDING,

                Price = price,
                MaxProfessionals = maxProfessionals,
                MaxAssistants = maxAssistants,
                MaxPatients = maxPatients,

                StartsAt = null,
                EndsAt = null
            };

            await _tenantSubscriptionRepository.AddAsync( subscription, cancellationToken );

            return subscription;
        }
    }
}