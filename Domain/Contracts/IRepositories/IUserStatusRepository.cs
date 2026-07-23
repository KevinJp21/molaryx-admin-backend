using Domain.Common;
using Domain.Entities;

namespace Domain.Contracts.IRepositories
{
    public interface IUserStatusRepository : IBaseRepository<UserStatus, short> {}
}