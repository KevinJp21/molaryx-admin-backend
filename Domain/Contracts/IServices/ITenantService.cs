using Application.DTOs.Tenant.TenantRegistration;
using Domain.Entities;

namespace Domain.Contracts.IServices
{
    public interface ITenantService
    {
        Task<Tenant> CreatePendingTenantAsync
        (
            TenantRegistrationDto dto,
            CancellationToken cancellationToken
        );
    }
}