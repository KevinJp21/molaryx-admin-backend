using Domain.Contracts.IRepositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRoleRepository(AppDbContext dbContext) : BaseRepository<UserRole, short>(dbContext), IUserRoleRepository {}
}