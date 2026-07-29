using Domain.Common;
using Domain.Entities;

namespace Domain.Contracts.IRepositories
{
    public interface ITenantRepository : IBaseRepository<Tenant, long>
    {
        Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken);

        Task<bool> ExistsByIdentificationNumberAsync(string identificationNumber, CancellationToken cancellationToken);

        Task<bool> ExistsByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken);
    }
}