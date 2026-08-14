using Application.Context;
using Application.Features.Patients.Command.CreatePatient;
using Application.Features.Patients.Command.UpdatePatient;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;

namespace Infrastructure.Services
{
    public class PatientService(
        IUnitOfWork _unitOfWork,
        ICurrentUser _currentUser
    ) : IPatientService
    {
        public async Task<bool> CreatePatientAsync(
            CreatePatientCommand request,
            CancellationToken cancellationToken
        )
        {
            var idTenant = _currentUser.IdTenant
                ?? throw new InvalidOperationException("El usuario no pertenece a un consultorio.");

            var tenant = await _unitOfWork.TenantRepository.GetByIdAsync(idTenant, cancellationToken)
                ?? throw new NotFoundException("El consultorio no existe.");

            if (tenant.IdTenantStatus != (short)TenantStatusEnum.ACTIVE)
            {
                throw new InvalidOperationException("El consultorio no está activo.");
            }

            var tenantSubscription = await _unitOfWork.TenantSubscriptionRepository
                .GetActiveSubscriptionAsync(idTenant, cancellationToken)
                ?? throw new NotFoundException("El consultorio no tiene una suscripción activa.");

            var patientCount = await _unitOfWork.PatientsRepository
                .CountPatientsByIdTenantAsync(idTenant, cancellationToken);

            if (tenantSubscription.MaxPatients is int maxPatients && patientCount >= maxPatients)
            {
                throw new InvalidOperationException("El consultorio ha alcanzado el número máximo de pacientes.");
            }

            var identificationExists = await _unitOfWork.PatientsRepository
                .ExistsByIdentificationNumberAsync(
                    idTenant,
                    request.IdentificationNumber,
                    cancellationToken
                );

            if (identificationExists)
            {
                throw new InvalidOperationException("Ya existe un paciente con ese número de identificación.");
            }

            var email = request.Email.Trim().ToLowerInvariant();

            var emailExists = await _unitOfWork.PatientsRepository
                .ExistsByEmailAsync(idTenant, email, cancellationToken);

            if (emailExists)
            {
                throw new InvalidOperationException("Ya existe un paciente con ese correo electrónico.");
            }

            var phoneExists = await _unitOfWork.PatientsRepository
                .ExistsByPhoneNumberAsync(idTenant, request.PhoneNumber, cancellationToken);

            if (phoneExists)
            {
                throw new InvalidOperationException("Ya existe un paciente con ese número de celular.");
            }

            var patient = new Patient
            {
                IdTenant = idTenant,
                IdIdentificationType = request.IdIdentificationType,
                IdentificationNumber = request.IdentificationNumber,
                FirstName = request.FirstName,
                SecondName = request.SecondName,
                FirstSurname = request.FirstSurname,
                SecondSurname = request.SecondSurname,
                BirthDate = request.BirthDate,
                PhoneNumber = request.PhoneNumber,
                Email = email,
                IsActive = true
            };

            await _unitOfWork.PatientsRepository.AddAsync(patient, cancellationToken);
            await _unitOfWork.PatientsRepository.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> UpdatePatientAsync(
            UpdatePatientCommand request,
            CancellationToken cancellationToken
        )
        {
            var idTenant = _currentUser.IdTenant
                ?? throw new InvalidOperationException("El usuario no pertenece a un consultorio.");

            var tenant = await _unitOfWork.TenantRepository.GetByIdAsync(idTenant, cancellationToken)
                ?? throw new NotFoundException("El consultorio no existe.");

            if (tenant.IdTenantStatus != (short)TenantStatusEnum.ACTIVE)
            {
                throw new InvalidOperationException("El consultorio no está activo.");
            }

            var patient = await _unitOfWork.PatientsRepository.GetByIdAsync(request.IdPatient, cancellationToken)
                ?? throw new NotFoundException("El paciente no existe.");

            if (patient.IdTenant != idTenant)
            {
                throw new InvalidOperationException("El paciente no pertenece a este consultorio.");
            }


            if (request.IdentificationNumber is not null)
            {
                var identificationExists = await _unitOfWork.PatientsRepository
                    .ExistsByIdentificationNumberAsync(
                        idTenant,
                        request.IdentificationNumber,
                        cancellationToken
                    );

                if (identificationExists)
                {
                    throw new InvalidOperationException("Ya existe un paciente con ese número de identificación.");
                }
            }

            patient.IdIdentificationType = request.IdIdentificationType ?? patient.IdIdentificationType;
            patient.IdentificationNumber = request.IdentificationNumber ?? patient.IdentificationNumber;
            patient.FirstName = request.FirstName ?? patient.FirstName;
            patient.SecondName = request.SecondName ?? patient.SecondName;
            patient.FirstSurname = request.FirstSurname ?? patient.FirstSurname;
            patient.SecondSurname = request.SecondSurname ?? patient.SecondSurname;
            patient.BirthDate = request.BirthDate ?? patient.BirthDate;
            patient.PhoneNumber = request.PhoneNumber ?? patient.PhoneNumber;
            patient.Email = request.Email ?? patient.Email;
            patient.IsActive = request.IsActive ?? patient.IsActive;

            await _unitOfWork.PatientsRepository.UpdateAsync(patient, cancellationToken);

            await _unitOfWork.PatientsRepository.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
