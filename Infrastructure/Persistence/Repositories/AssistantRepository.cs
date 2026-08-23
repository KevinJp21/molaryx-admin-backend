using Domain.Contracts.IRepositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class AssistantRepository(AppDbContext _context) : BaseRepository<Assistant, long>(_context), IAssistantRepository {}
}