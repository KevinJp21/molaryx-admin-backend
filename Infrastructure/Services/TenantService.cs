using Application.Common.Interfaces;
using Application.Features.Platform.Tenant.Command.UpdateTenant;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Specifications;

namespace Infrastructure.Services
{
    public class TenantService(
        IUnitOfWork unitOfWork
    ) : ITenantService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Tenant> CreatePendingTenantAsync<TTenantRegistration>(
            short idTenantType,
            TTenantRegistration dto,
            CancellationToken cancellationToken
            ) where TTenantRegistration : ITenantRegistration
        {

            var email = dto.Email.Trim().ToLowerInvariant();

            var emailExists = await _unitOfWork.TenantRepository.ExistsAsync(
                TenantSpec.ByEmail(email),
                cancellationToken
            );

            if (emailExists)
            {
                throw new InvalidOperationException("El correo electrónico del consultorio ya se encuentra registrado.");
            }

            if (!string.IsNullOrWhiteSpace(dto.IdentificationNumber))
            {
                var identificationNumberExists = await _unitOfWork
                    .TenantRepository.ExistsAsync(
                        TenantSpec.ByIdentificationNumber(dto.IdentificationNumber),
                        cancellationToken);

                if (identificationNumberExists)
                {
                    throw new InvalidOperationException("El número de identificación del consultorio ya se encuentra registrado.");
                }
            }

            var phoneNumberExists = await _unitOfWork.TenantRepository.ExistsAsync(
                TenantSpec.ByPhoneNumber(dto.PhoneNumber),
                cancellationToken
            );

            if (phoneNumberExists)
            {
                throw new InvalidOperationException("El número de telefono del consultorio ya se encuentra registrado.");
            }

            var tenant = new Tenant
            {
                IdTenantType = idTenantType,

                IdTenantStatus =
                    (short)TenantStatusEnum.PENDING,

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

        public async Task<Tenant> ActivateTenantAsync(
            long idTenant,
            CancellationToken cancellationToken)
        {
            var tenant = await _unitOfWork.TenantRepository
                .GetByIdAsync(
                    idTenant,
                    cancellationToken
                );

            if (tenant is null)
            {
                throw new InvalidOperationException("El consultorio no existe.");
            }

            if (tenant.IdTenantStatus != (short)TenantStatusEnum.PENDING)
            {
                throw new InvalidOperationException("El consultorio no se encuentra pendiente de aprobación.");
            }

            tenant.IdTenantStatus = (short)TenantStatusEnum.ACTIVE;

            await _unitOfWork.TenantRepository.UpdateAsync(tenant, cancellationToken);

            return tenant;
        }

        public async Task UpdateTenantAsync(
            long idTenant,
            UpdateTenantInfoRequest request,
            CancellationToken cancellationToken)
        {
            var tenant = await _unitOfWork.TenantRepository.GetByIdAsync(
                    idTenant,
                    cancellationToken,
                    TenantSpec.ById(idTenant))
                ?? throw new NotFoundException("El consultorio no existe.");

            if (request.Email is not null)
            {
                var email = request.Email.Trim().ToLowerInvariant();
                var emailExists = await _unitOfWork.TenantRepository.ExistsAsync(
                    TenantSpec.ByEmail(email),
                    cancellationToken);

                if (emailExists && !string.Equals(
                        tenant.Email,
                        email,
                        StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException(
                        "El correo electrónico del consultorio ya se encuentra registrado.");
                }
            }

            if (request.IdentificationNumber is not null)
            {
                var identificationNumber = request.IdentificationNumber.Trim();
                var identificationExists = await _unitOfWork.TenantRepository.ExistsAsync(
                    TenantSpec.ByIdentificationNumber(identificationNumber),
                    cancellationToken);

                if (identificationExists
                    && identificationNumber != tenant.IdentificationNumber)
                {
                    throw new InvalidOperationException(
                        "El número de identificación del consultorio ya se encuentra registrado.");
                }
            }

            if (request.PhoneNumber is not null)
            {
                var phoneNumber = request.PhoneNumber.Trim();
                var phoneExists = await _unitOfWork.TenantRepository.ExistsAsync(
                    TenantSpec.ByPhoneNumber(phoneNumber),
                    cancellationToken);

                if (phoneExists && phoneNumber != tenant.PhoneNumber)
                {
                    throw new InvalidOperationException(
                        "El número de telefono del consultorio ya se encuentra registrado.");
                }
            }

            tenant.IdIdentificationType =
                request.IdIdentificationType ?? tenant.IdIdentificationType;
            tenant.IdentificationNumber = request.IdentificationNumber is null
                ? tenant.IdentificationNumber
                : request.IdentificationNumber.Trim();
            tenant.ConsultoryName = request.ConsultoryName?.Trim() ?? tenant.ConsultoryName;
            tenant.Email = request.Email is null
                ? tenant.Email
                : request.Email.Trim().ToLowerInvariant();
            tenant.PhoneNumber = request.PhoneNumber?.Trim() ?? tenant.PhoneNumber;
            tenant.Address = request.Address?.Trim() ?? tenant.Address;
            tenant.IdTenantStatus = request.IdTenantStatus ?? tenant.IdTenantStatus;
            tenant.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.TenantRepository.UpdateAsync(tenant, cancellationToken);
        }
    }
}
