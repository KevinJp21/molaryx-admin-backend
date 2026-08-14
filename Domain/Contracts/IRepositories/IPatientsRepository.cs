using Domain.Common;
using Domain.Entities;

namespace Domain.Contracts.IRepositories
{
    public interface IPatientsRepository : IBaseRepository<Patient, long>
    {
    }
}
