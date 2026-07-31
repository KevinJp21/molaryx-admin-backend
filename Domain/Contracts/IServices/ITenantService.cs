using Application.Features.Tenant.Command.RegisterTenant;
using Domain.Entities;

namespace Domain.Contracts.IServices
{
    public interface ITenantService
    {
        Task<Tenant> CreatePendingTenantAsync
        (
            short idTenantType,
            TenantRegistration tenant,
            CancellationToken cancellationToken
        );

        Task<Tenant> ActivateTenantAsync
        (
            long IdTenant,
            CancellationToken cancellationToken
        );
    }
}