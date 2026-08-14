using Domain.Common;
using Domain.Entities;

namespace Domain.Contracts.IRepositories
{
    public interface IPasswordResetTokenRepository : IBaseRepository<PasswordResetToken, long>
    {
    }
}
