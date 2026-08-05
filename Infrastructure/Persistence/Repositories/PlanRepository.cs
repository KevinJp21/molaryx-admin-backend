using Domain.Common;
using Domain.Contracts.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class PlanRepository(AppDbContext dbContext) : BaseRepository<Plan, short>(dbContext), IPlanRepository
    {
    }
}