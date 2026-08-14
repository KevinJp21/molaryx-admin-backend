using Domain.Contracts.IRepositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class PromotionRepository(AppDbContext dbContext)
        : BaseRepository<Promotion, long>(dbContext), IPromotionRepository
    {
    }
}
