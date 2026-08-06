using Domain.Common;
using Domain.Entities;

namespace Domain.Contracts.IRepositories
{
    public interface IPlanRepository : IBaseRepository<Plan, short>
    {
        Task<List<Plan>> GetPublicPlansAsync(CancellationToken cancellationToken = default);
    }
}
