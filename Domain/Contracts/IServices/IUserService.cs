using Application.Common.Interfaces;
using Application.Features.Users.Command.CreateMember;
using Application.Features.Users.Command.UpdateMember;
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

        Task<(bool Success, string TemporaryPassword, string ConsultoryName)> CreateMemberAsync(
            CreateMemberCommand command,
            CancellationToken cancellationToken
        );

        Task<bool> UpdateMemberAsync(
            UpdateMemberCommand command,
            CancellationToken cancellationToken
        );
    }
}