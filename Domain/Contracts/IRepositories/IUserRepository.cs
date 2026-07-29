using Domain.Common;
using Domain.Entities;

namespace Domain.Contracts.IRepositories
{
    public interface IUserRepository : IBaseRepository<User, long>
    {
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

        Task<bool> ExistsByUsernameAsync(string username, CancellationToken cancellationToken);
        
        Task<bool> ExistsByEmailAsync( string email, CancellationToken cancellationToken );

        Task<bool> ExistsByIdentificationNumberAsync(string identificationNumber, CancellationToken cancellationToken);

        Task<bool> ExistsByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken);
    }
}