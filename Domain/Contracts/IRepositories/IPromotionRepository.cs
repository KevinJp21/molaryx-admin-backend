using Domain.Common;
using Domain.Entities;

namespace Domain.Contracts.IRepositories
{
    public interface IPromotionRepository : IBaseRepository<Promotion, long>
    {
        Task<Promotion?> GetActivePromotionAsync(
            string code,
            CancellationToken cancellationToken
        );

        Task<Promotion?> GetAvailablePromotionAsync(
            string code,
            CancellationToken cancellationToken
        );
    }
}