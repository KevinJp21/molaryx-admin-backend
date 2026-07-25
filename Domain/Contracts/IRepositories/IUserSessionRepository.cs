using Domain.Common;
using Domain.Entities;

namespace Domain.Contracts.IRepositories
{
    public interface IUserSessionRepository : IBaseRepository<UserSession, long>
    {
        Task<UserSession?> GetByRefreshTokenHashAsync(string refreshTokenHash, CancellationToken cancellationToken);

        Task<List<UserSession>> GetActiveSessionsByUserIdAsync(long idUser, CancellationToken cancellationToken);

        Task<(int count, List<UserSession> data)> GetAllSessionsByUserIdAsync(long idUser, bool? active, int page, int size, CancellationToken cancellationToken);

        Task<bool> RevokeSessionAsync(string refreshTokenHash, DateTime currentDate, CancellationToken cancellationToken);
    }
}