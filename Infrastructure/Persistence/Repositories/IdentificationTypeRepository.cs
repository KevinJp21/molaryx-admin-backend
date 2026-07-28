using Domain.Contracts.IRepositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class IdentificationTypeRepository(AppDbContext dbContext) : BaseRepository<IdentificationType, short>(dbContext), IIdentificationTypeRepository {}
}