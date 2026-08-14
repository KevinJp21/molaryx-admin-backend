using Domain.Common;
using Domain.Entities;

namespace Domain.Contracts.IRepositories
{
    public interface IServiceRepository : IBaseRepository<Service, long> {}
}