using Application.Common.Interfaces;
using Application.Features.Platform.Tenant.Command.UpdateTenant;
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

        Task UpdateTenantAsync(
            long idTenant,
            UpdateTenantInfoRequest request,
            CancellationToken cancellationToken);
    }
}
