using Domain.Contracts.IRepositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class PasswordResetTokenRepository(AppDbContext context)
        : BaseRepository<PasswordResetToken, long>(context), IPasswordResetTokenRepository
    {
    }
}
