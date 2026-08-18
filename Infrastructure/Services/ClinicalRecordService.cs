using Application.Context;
using Application.Features.ClinicalRecord.Command.CreateClinicalRecord;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Entities;

namespace Infrastructure.Services

{
    public class ClinicalRecordService(
        IUnitOfWork _unitOfWork,
        ICurrentUser _currentUser,
        ITenantAccessService _tenantAccessService,
        ITenantResourceService _tenantResourceService
    ) : IClinicalRecordService

    {
        public async Task<bool> CreateClinicalRecordAsync(CreateClinicalRecordCommand request, CancellationToken cancellationToken = default)

        {
            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);

            if (request.IdAppointment is not null)
            {
                await EnsureAppointmentAsync
                (
                    access.IdTenant,
                    request.IdPatient,
                    request.IdAppointment.Value,
                    request.IdService,
                    request.IdPatientTreatment,
                    cancellationToken
                );
            }

            if (request.IdPatientTreatment is not null)
            {
                await EnsurePatientTreatmentAsync(
                    access.IdTenant,
                    request.IdPatient,
                    request.IdPatientTreatment.Value,
                    cancellationToken
                );
            }

            if (request.IdService is not null)
            {
                await _tenantResourceService.RequireServiceAsync(
                    access.IdTenant,
                    request.IdService.Value,
                    cancellationToken
                );
            }

            var clinicalRecord = new ClinicalRecord
            {
                IdTenant = access.IdTenant,
                IdPatient = request.IdPatient,
                IdAppointment = request.IdAppointment,
                IdPatientTreatment = request.IdPatientTreatment,
                IdService = request.IdService,
                IdCreatedByUser = _currentUser.IdUser ?? throw new InvalidOperationException("El usuario no está autenticado."),
                RecordedAt = request.RecordedAt,
                Reason = request.Reason,
                Diagnosis = request.Diagnosis,
                Evolution = request.Evolution,
                Notes = request.Notes,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.ClinicalRecordRepository.AddAsync(clinicalRecord, cancellationToken);
            await _unitOfWork.ClinicalRecordRepository.SaveChangesAsync(cancellationToken);

            return true;
        }

        private async Task EnsureAppointmentAsync(
            long idTenant,
            long idPatient,
            long idAppointment,
            long? idService,
            long? idPatientTreatment,
            CancellationToken cancellationToken)
        {
            var appointment = await _tenantResourceService.RequireAppointmentAsync(
                idTenant,
                idAppointment,
                cancellationToken);

            if (appointment.IdPatient != idPatient)
            {
                throw new InvalidOperationException("La cita no pertenece a este paciente.");
            }

            if (idService is not null)
            {
                if (appointment.IdService != idService)
                {
                    throw new InvalidOperationException("El servicio no pertenece a esta cita.");
                }
            }

            if (idPatientTreatment is not null)
            {
                if (appointment.IdPatientTreatment != idPatientTreatment)
                {
                    throw new InvalidOperationException("El tratamiento no pertenece a esta cita.");
                }
            }
        }

        private async Task EnsurePatientTreatmentAsync(
            long idTenant,
            long idPatient,
            long idPatientTreatment,
            CancellationToken cancellationToken)
        {
            var patientTreatment = await _tenantResourceService.RequirePatientTreatmentAsync(
                idTenant,
                idPatientTreatment,
                cancellationToken);

            if (patientTreatment.IdPatient != idPatient)
            {
                throw new InvalidOperationException("El tratamiento no pertenece a este paciente.");
            }
        }
    }

}


