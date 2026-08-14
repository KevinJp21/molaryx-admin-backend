using Domain.Common;
using Domain.Contracts.IRepositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class PatientsRepository(AppDbContext context)
        : BaseRepository<Patient, long>(context), IPatientsRepository
    {
    }
}
