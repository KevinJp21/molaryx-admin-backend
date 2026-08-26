using Domain.Contracts.IRepositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class UserLegalAcceptanceRepository(AppDbContext context)
        : BaseRepository<UserLegalAcceptance, long>(context), IUserLegalAcceptanceRepository
    {
    }
}
