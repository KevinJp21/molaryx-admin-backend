using Application.Common.Interfaces;
using Application.Features.Tenant.Command.RegisterTenant;
using Domain.Entities;

namespace Domain.Contracts.IServices
{
    public interface IUserService
    {
        Task<User> CreatePendingOwnerAsync<TOwnerRegistration>
        (
            TOwnerRegistration owner,
            long idTenant,
            CancellationToken cancellationToken
        ) where TOwnerRegistration : IOwnerRegistration;

        Task<User> ActivateUserAsync
        (
            long idUser,
            CancellationToken cancellationToken
        );
    }
}