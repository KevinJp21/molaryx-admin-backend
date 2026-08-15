using Domain.Contracts.IRepositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class ProfessionalRepository(AppDbContext _context) : BaseRepository<Professional, long>(_context), IProfessionalRepository {}
}