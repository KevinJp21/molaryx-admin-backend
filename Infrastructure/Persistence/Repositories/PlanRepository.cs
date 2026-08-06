using Domain.Contracts.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class PlanRepository(AppDbContext dbContext) : BaseRepository<Plan, short>(dbContext), IPlanRepository
    {
        public async Task<List<Plan>> GetPublicPlansAsync(CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;

            return await DbSet
                .AsNoTracking()
                .Where(p => p.IsActive)
                .Include(p => p.PromotionPlans
                    .Where(pp =>
                        pp.Promotion.IsActive &&
                        pp.Promotion.StartsAt <= now &&
                        (pp.Promotion.EndsAt == null || pp.Promotion.EndsAt > now)))
                .ThenInclude(pp => pp.Promotion)
                .ToListAsync(cancellationToken);
        }
    }
}
