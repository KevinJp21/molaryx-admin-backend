using Application.Context;
using Application.Features.Patients.Command.CreatePatient;
using Application.Features.Patients.Command.UpdatePatient;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Specifications;

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
                .GetFirstAsync(
                    TenantSubscriptionSpec.ActiveByTenant(idTenant),
                    cancellationToken)
                ?? throw new NotFoundException("El consultorio no tiene una suscripción activa.");

            var patientCount = await _unitOfWork.PatientsRepository
                .CountAsync(PatientsSpec.ForTenantCount(idTenant), cancellationToken);

            if (tenantSubscription.MaxPatients is int maxPatients && patientCount >= maxPatients)
            {
                throw new InvalidOperationException("El consultorio ha alcanzado el número máximo de pacientes.");
            }

            var identificationExists = await _unitOfWork.PatientsRepository
                .ExistsAsync(
                    PatientsSpec.ByIdentificationNumber(idTenant, request.IdentificationNumber),
                    cancellationToken
                );

            if (identificationExists)
            {
                throw new InvalidOperationException("Ya existe un paciente con ese número de identificación.");
            }

            var email = request.Email.Trim().ToLowerInvariant();

            var emailExists = await _unitOfWork.PatientsRepository
                .ExistsAsync(PatientsSpec.ByEmail(idTenant, email), cancellationToken);

            if (emailExists)
            {
                throw new InvalidOperationException("Ya existe un paciente con ese correo electrónico.");
            }

            var phoneExists = await _unitOfWork.PatientsRepository
                .ExistsAsync(
                    PatientsSpec.ByPhoneNumber(idTenant, request.PhoneNumber),
                    cancellationToken
                );

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

            var patient = await _unitOfWork.PatientsRepository.GetByIdAsync(
                    request.IdPatient,
                    cancellationToken,
                    PatientsSpec.ById(request.IdPatient))
                ?? throw new NotFoundException("El paciente no existe.");

            if (patient.IdTenant != idTenant)
            {
                throw new InvalidOperationException("El paciente no pertenece a este consultorio.");
            }


            if (request.IdentificationNumber is not null)
            {
                var identificationExists = await _unitOfWork.PatientsRepository
                    .ExistsAsync(
                        PatientsSpec.ByIdentificationNumber(idTenant, request.IdentificationNumber),
                        cancellationToken
                    );

                if (identificationExists && request.IdentificationNumber != patient.IdentificationNumber)
                {
                    throw new InvalidOperationException("Ya existe un paciente con ese número de identificación.");
                }
            }

            if (request.PhoneNumber is not null)
            {
                var phoneExists = await _unitOfWork.PatientsRepository.ExistsAsync(
                    PatientsSpec.ByPhoneNumber(idTenant, request.PhoneNumber),
                    cancellationToken);

                if (phoneExists && request.PhoneNumber != patient.PhoneNumber)
                {
                    throw new InvalidOperationException("Ya existe un paciente con ese número de celular.");
                }
            }


            if (request.Email is not null)
            {
                var emailExists = await _unitOfWork.PatientsRepository.ExistsAsync(
                    PatientsSpec.ByEmail(idTenant, request.Email),
                    cancellationToken);

                if (emailExists && request.Email != patient.Email)
                {
                    throw new InvalidOperationException("Ya existe un paciente con ese correo electrónico.");
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
            patient.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.PatientsRepository.UpdateAsync(patient, cancellationToken);

            await _unitOfWork.PatientsRepository.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> DeletePatientAsync(
            long idPatient,
            CancellationToken cancellationToken
        )
        {
            var patient = await _unitOfWork.PatientsRepository.GetByIdAsync(idPatient, cancellationToken)
                ?? throw new NotFoundException("El paciente no existe.");

            if (patient.IdTenant != _currentUser.IdTenant)
            {
                throw new InvalidOperationException("El paciente no pertenece a este consultorio.");
            }


            patient.DeletedAt = DateTime.UtcNow;


            await _unitOfWork.PatientsRepository.UpdateAsync(patient, cancellationToken);

            await _unitOfWork.PatientsRepository.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
