using Domain.Contracts.IRepositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class TenantStatusRepository(AppDbContext context) : BaseRepository<TenantStatus, short>(context), ITenantStatusRepository {}
}