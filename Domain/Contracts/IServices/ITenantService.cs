using Application.Features.Tenant.Command.RegisterTenant;
using Domain.Entities;

namespace Domain.Contracts.IServices
{
    public interface ITenantService
    {
        Task<Tenant> CreatePendingTenantAsync
        (
            TenantRegistration tenant,
            CancellationToken cancellationToken
        );
    }
}