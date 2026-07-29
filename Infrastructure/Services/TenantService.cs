using Application.Features.Tenant.Command.RegisterTenant;
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
            TenantRegistration dto,
            CancellationToken cancellationToken)
        {

            var email = dto.Email.Trim().ToLowerInvariant();

            var emailExists = await _unitOfWork.TenantRepository.ExistsByEmailAsync(
                email,
                cancellationToken
            );

            if (emailExists)
            {
                throw new InvalidOperationException("El correo electrónico ya se encuentra registrado.");
            }

            if (!string.IsNullOrWhiteSpace(dto.IdentificationNumber))
            {
                var identificationNumberExists = await _unitOfWork
                    .TenantRepository.ExistsByIdentificationNumberAsync(
                        dto.IdentificationNumber,
                        cancellationToken);

                if (identificationNumberExists)
                {
                    throw new InvalidOperationException("El número de identificación ya se encuentra registrado.");
                }
            }

            var phoneNumberExists = await _unitOfWork.TenantRepository.ExistsByPhoneNumberAsync(
                dto.PhoneNumber,
                cancellationToken
            );

            if (phoneNumberExists)
            {
                throw new InvalidOperationException("El número de telefono ya se encuentra registrado.");
            }

            var tenant = new Tenant
            {
                IdTenantType = (short)TenantTypeEnum.STANDARD,

                IdTenantStatus =
                    (short)TenantStatusEnum.PENDING_APPROVAL,

                IdIdentificationType = dto.IdIdentificationType,
                IdentificationNumber = dto.IdentificationNumber,

                ConsultoryName = dto.ConsultoryName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
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