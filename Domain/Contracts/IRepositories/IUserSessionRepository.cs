using Domain.Common;
using Domain.Entities;

namespace Domain.Contracts.IRepositories
{
    public interface IUserSessionRepository : IBaseRepository<UserSession, long>
    {
        Task<bool> RevokeSessionAsync(long idUser, string refreshTokenHash, DateTime currentDate, CancellationToken cancellationToken);
    }
}
