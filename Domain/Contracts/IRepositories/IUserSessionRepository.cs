using Domain.Common;
using Domain.Entities;

namespace Domain.Contracts.IRepositories
{
    public interface IUserSessionRepository : IBaseRepository<UserSession, long>
    {
        Task<UserSession?> GetByRefreshTokenHashAsync(string refreshTokenHash, CancellationToken cancellationToken);

        Task<IEnumerable<UserSession>> GetActiveSessionsByUserIdAsync(long idUser, CancellationToken cancellationToken);

        Task<bool> RevokeSessionAsync(string refreshTokenHash, DateTime currentDate, CancellationToken cancellationToken);
    }
}