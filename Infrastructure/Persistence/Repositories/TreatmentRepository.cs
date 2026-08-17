using Domain.Contracts.IRepositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class TreatmentRepository(
        AppDbContext context
    ) : BaseRepository<Treatment, long>(context), ITreatmentRepository
    {
    }
}
