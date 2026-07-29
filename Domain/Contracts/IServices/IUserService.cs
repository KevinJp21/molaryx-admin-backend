using Application.Features.Tenant.Command.RegisterTenant;
using Domain.Entities;

namespace Domain.Contracts.IServices
{
    public interface IUserService
    {
        Task<User> CreatePendingOwnerAsync
        (
            OwnerRegistration owner,
            long idTenant,
            CancellationToken cancellationToken
        );
    }
}