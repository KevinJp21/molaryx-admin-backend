using Domain.Contracts.IRepositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class PatientTreatmentRepository(AppDbContext context)
        : BaseRepository<PatientTreatment, long>(context), IPatientTreatmentRepository
    {
    }
}
