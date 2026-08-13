using Domain.Common;
using Domain.Entities;

namespace Domain.Contracts.IRepositories
{
    public interface IPatientsRepository : IBaseRepository<Patient, long>
    {
        Task<(int totalItems, List<Patient> data)> GetAllPatientsByIdTenantAsync(
            long idTenant,
            int page,
            int size,
            ISpecification<Patient>? spec = null,
            CancellationToken cancellationToken = default
        );

        Task<int> CountActivePatientsByIdTenantAsync(long idTenant, CancellationToken cancellationToken);

        Task<bool> ExistsByEmailAsync(
            long idTenant,
            string email,
            CancellationToken cancellationToken = default
        );

        Task<bool> ExistsByIdentificationNumberAsync(
            long idTenant,
            string identificationNumber,
            CancellationToken cancellationToken = default
        );

        Task<bool> ExistsByPhoneNumberAsync(
            long idTenant,
            string phoneNumber,
            CancellationToken cancellationToken = default
        );

    }
}