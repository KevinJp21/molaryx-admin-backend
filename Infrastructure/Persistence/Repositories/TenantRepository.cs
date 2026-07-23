using Domain.Contracts.IRepositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class TenantRepository(AppDbContext dbContext) : BaseRepository<Tenant, long>(dbContext), ITenantRepository {}
}