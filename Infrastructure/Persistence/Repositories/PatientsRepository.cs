using Domain.Common;
using Domain.Contracts.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class PatientsRepository(AppDbContext context) : BaseRepository<Patient, long>(context), IPatientsRepository
    {
        public async Task<(int totalItems, List<Patient> data)> GetAllPatientsByIdTenantAsync(
            long idTenant,
            int page,
            int size,
            ISpecification<Patient>? spec = null,
            CancellationToken cancellationToken = default
        )
        {
            var query = DbSet
                .AsNoTracking()
                .Where(p => p.IdTenant == idTenant)
                .Where(spec?.Criteria ?? (_ => true));

            var totalItems = await query.CountAsync(cancellationToken);

            var patients = await query
                .OrderByDescending(p => p.CreatedAt)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(cancellationToken);

            return (totalItems, patients);
        }
    }
}