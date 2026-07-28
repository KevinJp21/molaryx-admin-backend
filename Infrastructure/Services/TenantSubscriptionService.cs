using Domain.Constants;
using Domain.Contracts;
using Domain.Contracts.IRepositories;
using Domain.Contracts.IServices;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Constants;

namespace Infrastructure.Services
{
    public class TenantSubscriptionService(
        IUnitOfWork unitOfWork
    ) : ITenantSubscriptionService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<TenantSubscription> CreateSubscriptionAsync(long idTenant, short idPlan, CancellationToken cancellationToken)
        {
            var plan = await _unitOfWork.PlanRepository.GetByIdAsync(idPlan, cancellationToken);

            if (plan is null || !plan.IsActive)
            {
                throw new InvalidOperationException("El plan seleccionado no está disponible.");
            }

            var activeSubscription = await _unitOfWork.TenantSubscriptionRepository.GetActiveSubscriptionAsync(idTenant, cancellationToken);

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

            await _unitOfWork.TenantSubscriptionRepository.AddAsync(subscription, cancellationToken);

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
            var tenant = await _unitOfWork.TenantRepository.GetByIdAsync(idTenant, cancellationToken);

            if (tenant is null)
            {
                throw new InvalidOperationException("El consultorio no existe.");
            }

            var plan = await _unitOfWork.PlanRepository.GetByIdAsync(idPlan);

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

            var activeSubscription = await _unitOfWork.TenantSubscriptionRepository.GetActiveSubscriptionAsync(
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

            await _unitOfWork.TenantSubscriptionRepository.AddAsync( subscription, cancellationToken );

            return subscription;
        }
    }
}