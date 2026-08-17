using Application.Features.PatientTreatment.Command.CreatePatientTreatment;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Specifications;

namespace Infrastructure.Services
{
    public class PatientTreatmentService(
        IUnitOfWork _unitOfWork,
        ITenantAccessService _tenantAccessService
    ) : IPatientTreatmentService
    {
        public async Task<bool> CreatePatientTreatmentAsync(
            CreatePatientTreatmentCommand request,
            CancellationToken cancellationToken)
        {
            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);
            var idTenant = access.IdTenant;

            await EnsurePatientAsync(idTenant, request.IdPatient, cancellationToken);
            await EnsureTreatmentAsync(idTenant, request.IdTreatment, cancellationToken);

            var alreadyAssigned = await _unitOfWork.PatientTreatmentRepository.ExistsAsync(
                PatientTreatmentsSpec.ActiveByPatientAndTreatment(
                    idTenant,
                    request.IdPatient,
                    request.IdTreatment),
                cancellationToken);

            if (alreadyAssigned)
            {
                throw new InvalidOperationException(
                    "El paciente ya tiene este tratamiento activo o pausado.");
            }

            var patientTreatment = new PatientTreatment
            {
                IdTenant = idTenant,
                IdPatient = request.IdPatient,
                IdTreatment = request.IdTreatment,
                AgreedPrice = request.AgreedPrice,
                IdPaymentFrequency = request.IdPaymentFrequency,
                PeriodicAmount = request.PeriodicAmount,
                StartAt = request.StartAt,
                EndAt = request.EndAt,
                IdTreatmentStatus = (short)TreatmentStatusEnum.ACTIVE,
                Notes = string.IsNullOrWhiteSpace(request.Notes)
                    ? null
                    : request.Notes.Trim(),
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.PatientTreatmentRepository.AddAsync(patientTreatment, cancellationToken);
            await _unitOfWork.PatientTreatmentRepository.SaveChangesAsync(cancellationToken);

            return true;
        }

        private async Task EnsurePatientAsync(
            long idTenant,
            long idPatient,
            CancellationToken cancellationToken)
        {
            var patient = await _unitOfWork.PatientsRepository.GetByIdAsync(
                    idPatient,
                    cancellationToken,
                    PatientsSpec.ById(idPatient))
                ?? throw new NotFoundException("El paciente no existe.");

            if (patient.IdTenant != idTenant)
            {
                throw new InvalidOperationException("El paciente no pertenece a este consultorio.");
            }
        }

        private async Task EnsureTreatmentAsync(
            long idTenant,
            long idTreatment,
            CancellationToken cancellationToken)
        {
            var treatment = await _unitOfWork.TreatmentRepository.GetByIdAsync(
                    idTreatment,
                    cancellationToken,
                    TreatmentsSpec.ById(idTreatment))
                ?? throw new NotFoundException("El tratamiento no existe.");

            if (treatment.IdTenant != idTenant || !treatment.IsActive)
            {
                throw new InvalidOperationException(
                    "El tratamiento no está disponible en este consultorio.");
            }
        }
    }
}
