using Application.DTOs.Tenant.TenantRegistration;
using Domain.Entities;

namespace Domain.Contracts.IServices
{
    public interface IUserService
    {
        Task<User> CreatePendingOwnerAsync
        (
            OwnerRegistrationDto dto,
            long idTenant,
            CancellationToken cancellationToken
        );
    }
}