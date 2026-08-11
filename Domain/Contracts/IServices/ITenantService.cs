using Application.Common.Interfaces;
using Domain.Entities;

namespace Domain.Contracts.IServices
{
    public interface ITenantService
    {
        Task<Tenant> CreatePendingTenantAsync<TTenantRegistration>
        (
            short idTenantType,
            TTenantRegistration tenant,
            CancellationToken cancellationToken
        ) where TTenantRegistration : ITenantRegistration;

        Task<Tenant> ActivateTenantAsync
        (
            long IdTenant,
            CancellationToken cancellationToken
        );
    }
}