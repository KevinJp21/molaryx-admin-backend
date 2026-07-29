using Domain.Contracts.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class TenantRepository(AppDbContext dbContext) : BaseRepository<Tenant, long>(dbContext), ITenantRepository
    {
        public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await DbSet
                .AnyAsync
                (
                    t => t.Email == email,
                    cancellationToken
                );
        }

        public async Task<bool> ExistsByIdentificationNumberAsync(string identificationNumber, CancellationToken cancellationToken)
        {
            return await DbSet
                .AnyAsync
                (
                    u => u.IdentificationNumber == identificationNumber,
                    cancellationToken
                );
        }
        
        public async Task<bool> ExistsByPhoneNumberAsync( string phoneNumber, CancellationToken cancellationToken)
        {
            return await DbSet
                .AnyAsync
                (
                    u => u.PhoneNumber == phoneNumber,
                    cancellationToken
                );
        }
    }
}