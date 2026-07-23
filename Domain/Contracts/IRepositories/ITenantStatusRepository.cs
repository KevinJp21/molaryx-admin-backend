using Domain.Common;
using Domain.Entities;

namespace Domain.Contracts.IRepositories
{
    public interface ITenantStatusRepository : IBaseRepository<TenantStatus, short> {}
}