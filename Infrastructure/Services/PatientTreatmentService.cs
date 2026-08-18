using Application.Features.PatientTreatment.Command.CreatePatientTreatment;
using Application.Features.PatientTreatment.Command.UpdatePatientTreatment;
using Domain.Common;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Entities;
using Domain.Enums;
using Domain.Specifications;

namespace Infrastructure.Services
{
    public class PatientTreatmentService(
        IUnitOfWork _unitOfWork,
        ITenantAccessService _tenantAccessService,
        ITenantResourceService _tenantResourceService
    ) : IPatientTreatmentService
    {
        public async Task<bool> CreatePatientTreatmentAsync(
            CreatePatientTreatmentCommand request,
            CancellationToken cancellationToken)
        {
            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);
            var idTenant = access.IdTenant;

            await _tenantResourceService.RequirePatientAsync(idTenant, request.IdPatient, cancellationToken);
            await _tenantResourceService.RequireTreatmentAsync(idTenant, request.IdTreatment, cancellationToken);

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

        public async Task<bool> UpdatePatientTreatmentAsync(
            UpdatePatientTreatmentCommand request,
            CancellationToken cancellationToken)
        {
            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);
            var idTenant = access.IdTenant;

            var patientTreatment = await _tenantResourceService.RequirePatientTreatmentAsync(
                idTenant,
                request.IdPatientTreatment,
                cancellationToken);

            TreatmentStatusRules.EnsureCanEdit(patientTreatment.IdTreatmentStatus);

            var agreedPrice = request.AgreedPrice;
            var idPaymentFrequency = request.IdPaymentFrequency;
            var periodicAmount = request.PeriodicAmount;
            var startAt = request.StartAt ?? patientTreatment.StartAt;
            var idTreatmentStatus = request.IdTreatmentStatus ?? patientTreatment.IdTreatmentStatus;
            var notes = request.Notes is null
                ? patientTreatment.Notes
                : string.IsNullOrWhiteSpace(request.Notes)
                    ? null
                    : request.Notes.Trim();

            if (!idPaymentFrequency.HasValue
                || idPaymentFrequency.Value == (short)PaymentFrequencyEnum.ONE_TIME)
            {
                periodicAmount = null;
            }

            if (request.IdTreatmentStatus.HasValue)
            {
                TreatmentStatusRules.EnsureCanTransition(
                    patientTreatment.IdTreatmentStatus,
                    request.IdTreatmentStatus.Value);
            }

            if (idPaymentFrequency.HasValue
                && idPaymentFrequency.Value != (short)PaymentFrequencyEnum.ONE_TIME
                && (periodicAmount is null || periodicAmount <= 0))
            {
                throw new InvalidOperationException(
                    "El monto periódico es obligatorio para esta frecuencia de pago.");
            }

            if (TreatmentStatusRules.IsFinal(idTreatmentStatus)
                && idTreatmentStatus != patientTreatment.IdTreatmentStatus)
            {
                patientTreatment.EndAt = DateTime.UtcNow;
            }

            patientTreatment.AgreedPrice = agreedPrice;
            patientTreatment.IdPaymentFrequency = idPaymentFrequency;
            patientTreatment.PeriodicAmount = periodicAmount;
            patientTreatment.StartAt = startAt;
            patientTreatment.IdTreatmentStatus = idTreatmentStatus;
            patientTreatment.Notes = notes;
            patientTreatment.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.PatientTreatmentRepository.UpdateAsync(patientTreatment, cancellationToken);
            await _unitOfWork.PatientTreatmentRepository.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
