using Domain.Common;
using Domain.Entities;

namespace Domain.Contracts.IRepositories
{
    public interface IPasswordResetTokenRepository : IBaseRepository<PasswordResetToken, long>
    {
        Task<PasswordResetToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default);


        Task<List<PasswordResetToken>> GetActiveTokensByUserIdAsync(long userId, CancellationToken cancellationToken = default);
    }
}