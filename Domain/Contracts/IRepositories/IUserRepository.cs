using Domain.Common;
using Domain.Entities;

namespace Domain.Contracts.IRepositories
{
    public interface IUserRepository : IBaseRepository<User, long>
    {
        Task<List<Permission>> GetPermissionsByUserIdAsync(long userId, CancellationToken cancellationToken);
    }
}
