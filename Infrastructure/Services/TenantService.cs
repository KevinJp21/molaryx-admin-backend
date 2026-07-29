using Application.DTOs.Tenant.TenantRegistration;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Services
{
    public class TenantService(
        IUnitOfWork unitOfWork
    ) : ITenantService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

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

            await _unitOfWork.TenantRepository.AddAsync(
                tenant,
                cancellationToken
            );

            return tenant;
        }
    }
}