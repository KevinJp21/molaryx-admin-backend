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
    }
}