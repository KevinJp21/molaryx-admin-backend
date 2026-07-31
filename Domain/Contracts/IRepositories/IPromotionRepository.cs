using Domain.Common;
using Domain.Entities;

namespace Domain.Contracts.IRepositories
{
    public interface IPromotionRepository : IBaseRepository<Promotion, long>
    {
        Task<Promotion?> GetActivePromotionAsync(
            long idPromotion,
            CancellationToken cancellationToken
        );

        Task<Promotion?> GetAvailablePromotionAsync(
            long idPromotion,
            CancellationToken cancellationToken
        );
    }
}