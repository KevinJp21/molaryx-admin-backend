using Application.DTOs.Tenant.TenantRegistration;
using Domain.Contracts.IRepositories;
using Domain.Contracts.IServices;
using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Services
{
    public class TenantService(
        ITenantRepository tenantRepository
    ) : ITenantService
    {
        private readonly ITenantRepository _tenantRepository = tenantRepository;

        public async Task<Tenant> CreatePendingTenantAsync(
            TenantRegistrationDto dto,
            CancellationToken cancellationToken)
        {
            var tenant = new Tenant
            {
                IdTenantType = (short)TenantTypeEnum.STANDARD,

                IdTenantStatus =
                    (short)TenantStatusEnum.PENDING_APPROVAL,

                IdIdentificationType = dto.IdIdentificationType,
                IdentificationNumber = dto.IdentificationNumber,

                ConsultoryName = dto.ConsultoryName,
                Email = dto.Email,
                CellPhone = dto.CellPhone,
                Address = dto.Address
            };

            await _tenantRepository.AddAsync(
                tenant,
                cancellationToken
            );

            return tenant;
        }
    }
}