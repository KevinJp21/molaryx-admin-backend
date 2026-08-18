using Domain.Contracts.IRepositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class ClinicalRecordRepository(AppDbContext context) : BaseRepository<ClinicalRecord, long>(context), IClinicalRecordRepository
    {
    }
}