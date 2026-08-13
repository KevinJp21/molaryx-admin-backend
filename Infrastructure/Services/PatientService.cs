using Application.Context;
using Application.Features.Patients.Command.CreatePatient;
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
                .CountActivePatientsByIdTenantAsync(idTenant, cancellationToken);

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
    }
}
