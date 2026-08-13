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

            foreach (var include in spec?.Includes ?? [])
            {
                query = query.Include(include);
            }

            foreach (var includePath in spec?.IncludePaths ?? [])
            {
                query = query.Include(includePath);
            }

            var totalItems = await query.CountAsync(cancellationToken);

            var patients = await query
                .OrderByDescending(p => p.CreatedAt)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(cancellationToken);

            return (totalItems, patients);
        }

        public async Task<int> CountActivePatientsByIdTenantAsync(long idTenant, CancellationToken cancellationToken)
        {
            return await DbSet
                .Where(p => p.IdTenant == idTenant && p.IsActive)
                .CountAsync(cancellationToken);
        }

        public async Task<bool> ExistsByEmailAsync(
            long idTenant,
            string email,
            CancellationToken cancellationToken = default)
        {
            return await DbSet.AnyAsync(
                p => p.IdTenant == idTenant && p.Email == email,
                cancellationToken
            );
        }

        public async Task<bool> ExistsByIdentificationNumberAsync(
            long idTenant,
            string identificationNumber,
            CancellationToken cancellationToken = default)
        {
            return await DbSet.AnyAsync(
                p => p.IdTenant == idTenant
                    && p.IdentificationNumber == identificationNumber,
                cancellationToken
            );
        }

        public async Task<bool> ExistsByPhoneNumberAsync(
            long idTenant,
            string phoneNumber,
            CancellationToken cancellationToken = default)
        {
            return await DbSet.AnyAsync(
                p => p.IdTenant == idTenant && p.PhoneNumber == phoneNumber,
                cancellationToken
            );
        }
    }
}