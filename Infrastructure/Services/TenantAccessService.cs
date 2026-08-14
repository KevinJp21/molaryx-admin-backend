using Application.Context;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Specifications;

namespace Infrastructure.Services
{
    public class TenantAccessService(
        IUnitOfWork _unitOfWork,
        ICurrentUser _currentUser
    ) : ITenantAccessService
    {
        public async Task<TenantAccessContext> RequireActiveAsync(
            CancellationToken cancellationToken = default)
        {
            var idTenant = _currentUser.IdTenant
                ?? throw new InvalidOperationException("El usuario no pertenece a un consultorio.");

            var tenant = await _unitOfWork.TenantRepository.GetByIdAsync(idTenant, cancellationToken)
                ?? throw new NotFoundException("El consultorio no existe.");

            if (tenant.IdTenantStatus != (short)TenantStatusEnum.ACTIVE)
            {
                throw new InvalidOperationException("El consultorio no está activo.");
            }

            var subscription = await _unitOfWork.TenantSubscriptionRepository
                .GetFirstAsync(
                    TenantSubscriptionSpec.ActiveByTenant(idTenant),
                    cancellationToken)
                ?? throw new NotFoundException("El consultorio no tiene una suscripción activa.");

            return new TenantAccessContext(idTenant, tenant, subscription);
        }
    }
}
