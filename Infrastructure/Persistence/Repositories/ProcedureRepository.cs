using Domain.Contracts.IRepositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class ProcedureRepository(
        AppDbContext context
    ) : BaseRepository<Procedure, long>(context), IProcedureRepository
    {
    }
}
