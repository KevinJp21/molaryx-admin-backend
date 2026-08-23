using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Specifications;

namespace Application.Features.ClinicalRecord.Query.GetClinicalRecords
{
    public class GetClinicalRecordsQueryHandler(
        IUnitOfWork _unitOfWork,
        ITenantAccessService _tenantAccessService
    ) : IRequestHandler<GetClinicalRecordsQuery, PagedResult<GetClinicalRecordsResponse>>
    {
        public async Task<PagedResult<GetClinicalRecordsResponse>> Handle(
            GetClinicalRecordsQuery request,
            CancellationToken cancellationToken = default)
        {
            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);

            var spec = new ClinicalRecordSpec(
                access.IdTenant,
                request.IdPatient,
                request.IdAppointment,
                request.IdPatientTreatment);

            var (totalItems, clinicalRecords) = await _unitOfWork.ClinicalRecordRepository.GetPagedAsync(
                PaginationHelper.GetEffectivePage(request.Page),
                PaginationHelper.GetEffectivePageSize(request.Size),
                spec,
                cancellationToken
            );

            return new PagedResult<GetClinicalRecordsResponse>
            {
                Items = [.. clinicalRecords.Select(MapClinicalRecord)],
                Page = PaginationHelper.GetEffectivePage(request.Page),
                Size = PaginationHelper.GetEffectivePageSize(request.Size),
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(
                    totalItems / (double)PaginationHelper.GetEffectivePageSize(request.Size)
                )
            };
        }

        private static GetClinicalRecordsResponse MapClinicalRecord(Domain.Entities.ClinicalRecord clinicalRecord)
        {
            return new GetClinicalRecordsResponse
            {
                IdClinicalRecord = clinicalRecord.IdClinicalRecord,
                IdAppointment = clinicalRecord.IdAppointment,
                IdPatientTreatment = clinicalRecord.IdPatientTreatment,
                IdProcedure = clinicalRecord.IdProcedure,
                ProcedureName = clinicalRecord.Procedure?.Name,
                RecordedAt = clinicalRecord.RecordedAt,
                Reason = clinicalRecord.Reason,
                Diagnosis = clinicalRecord.Diagnosis,
                Evolution = clinicalRecord.Evolution,
                Notes = clinicalRecord.Notes,
                Patient = MapPatient(clinicalRecord.Patient),
                CreatedBy = MapCreatedBy(clinicalRecord.CreatedByUser),
                Appointment = MapAppointment(clinicalRecord.Appointment),
                PatientTreatment = MapPatientTreatment(clinicalRecord.PatientTreatment)
            };
        }

        private static ClinicalRecordPatient MapPatient(Domain.Entities.Patient patient)
        {
            return new ClinicalRecordPatient
            {
                IdPatient = patient.IdPatient,
                IdentificationType = patient.IdentificationType.Name,
                IdentificationNumber = patient.IdentificationNumber,
                Name = $"{patient.FirstName}{(!string.IsNullOrEmpty(patient.SecondName) ? $" {patient.SecondName}" : string.Empty)}",
                Surname = $"{patient.FirstSurname}{(!string.IsNullOrEmpty(patient.SecondSurname) ? $" {patient.SecondSurname}" : string.Empty)}",
                Email = patient.Email,
                PhoneNumber = patient.PhoneNumber
            };
        }

        private static ClinicalRecordCreatedBy MapCreatedBy(Domain.Entities.User user)
        {
            return new ClinicalRecordCreatedBy
            {
                IdUser = user.IdUser,
                Name = $"{user.FirstName}{(!string.IsNullOrEmpty(user.SecondName) ? $" {user.SecondName}" : string.Empty)}",
                Surname = $"{user.FirstSurname}{(!string.IsNullOrEmpty(user.SecondSurname) ? $" {user.SecondSurname}" : string.Empty)}"
            };
        }

        private static ClinicalRecordAppointment? MapAppointment(Domain.Entities.Appointment? appointment)
        {
            if (appointment is null)
            {
                return null;
            }

            return new ClinicalRecordAppointment
            {
                IdAppointment = appointment.IdAppointment,
                ProcedureNames = string.Join(
                    ", ",
                    appointment.AppointmentProcedures.Select(p => p.Procedure.Name)),
                IdAppointmentStatus = appointment.IdAppointmentStatus,
                AppointmentStatus = appointment.AppointmentStatus.Name,
                StartAt = appointment.StartAt,
                EndAt = appointment.EndAt,
                ProfessionalName = $"{appointment.Professional.User.FirstName}{(!string.IsNullOrEmpty(appointment.Professional.User.SecondName) ? $" {appointment.Professional.User.SecondName}" : string.Empty)}",
                ProfessionalSurname = $"{appointment.Professional.User.FirstSurname}{(!string.IsNullOrEmpty(appointment.Professional.User.SecondSurname) ? $" {appointment.Professional.User.SecondSurname}" : string.Empty)}"
            };
        }

        private static ClinicalRecordPatientTreatment? MapPatientTreatment(
            Domain.Entities.PatientTreatment? patientTreatment)
        {
            if (patientTreatment is null)
            {
                return null;
            }

            return new ClinicalRecordPatientTreatment
            {
                IdPatientTreatment = patientTreatment.IdPatientTreatment,
                IdTreatment = patientTreatment.IdTreatment,
                TreatmentName = patientTreatment.Treatment.Name,
                AgreedPrice = patientTreatment.AgreedPrice,
                IdPatientTreatmentStatus = patientTreatment.IdPatientTreatmentStatus,
                PatientTreatmentStatus = patientTreatment.PatientTreatmentStatus.Name,
                StartAt = patientTreatment.StartAt,
                EndAt = patientTreatment.EndAt
            };
        }
    }
}
