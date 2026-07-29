using Domain.Contracts.IRepositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class PlanRepository(AppDbContext dbContext) : BaseRepository<Plan, short>(dbContext), IPlanRepository {}
}