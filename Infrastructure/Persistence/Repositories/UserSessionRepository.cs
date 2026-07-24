using Domain.Contracts.IRepositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class UserSessionRepository(AppDbContext dbContext) : BaseRepository<UserSession, long>(dbContext), IUserSessionRepository {}
}