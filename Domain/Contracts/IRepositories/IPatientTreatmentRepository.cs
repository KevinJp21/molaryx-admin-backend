using Domain.Common;
using Domain.Entities;

namespace Domain.Contracts.IRepositories
{
    public interface IPatientTreatmentRepository : IBaseRepository<PatientTreatment, long>
    {
    }
}
