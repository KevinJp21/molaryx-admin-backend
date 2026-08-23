using Application.Common.Mediator.Interfaces;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Entities;
using Domain.Models;
using Domain.Specifications;
using Shared.Utils;

namespace Application.Features.ClinicalRecord.Query.GetClinicalHistory
{
    public class GetClinicalHistoryQueryHandler(
        IUnitOfWork _unitOfWork,
        ITenantAccessService _tenantAccessService,
        ITenantResourceService _tenantResourceService,
        IClinicalHistoryPdfService _clinicalHistoryPdfService
    ) : IRequestHandler<GetClinicalHistoryQuery, (byte[] Content, string FileName)>
    {
        public async Task<(byte[] Content, string FileName)> Handle(
            GetClinicalHistoryQuery request,
            CancellationToken cancellationToken = default)
        {
            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);

            var patient = await _tenantResourceService.RequirePatientAsync(
                access.IdTenant,
                request.IdPatient,
                cancellationToken);

            var patientIdentificationType = await _unitOfWork.IdentificationTypeRepository.GetByIdAsync(
                patient.IdIdentificationType,
                cancellationToken);

            string? tenantIdentificationType = null;
            if (access.Tenant.IdIdentificationType is not null)
            {
                tenantIdentificationType = (await _unitOfWork.IdentificationTypeRepository.GetByIdAsync(
                    access.Tenant.IdIdentificationType.Value,
                    cancellationToken))?.Name;
            }

            var recordedFrom = NormalizeFrom(request.From);
            var recordedToInclusive = NormalizeToInclusive(request.To);

            var spec = new ClinicalRecordSpec(
                access.IdTenant,
                request.IdPatient,
                recordedFrom: recordedFrom,
                recordedToInclusive: recordedToInclusive,
                orderAscending: false);

            var records = await _unitOfWork.ClinicalRecordRepository.GetAll(spec, cancellationToken)
                ?? [];

            var templateInformation = BuildTemplateInformation(
                access.Tenant,
                tenantIdentificationType,
                patient,
                patientIdentificationType?.Name ?? string.Empty,
                request.From,
                request.To,
                records);

            return (
                _clinicalHistoryPdfService.GenerateClinicalHistoryTemplate(templateInformation),
                BuildFileName(patient));
        }

        private static ClinicalHistoryTemplateInformation BuildTemplateInformation(
            Tenant tenant,
            string? tenantIdentificationType,
            Patient patient,
            string patientIdentificationType,
            DateOnly? from,
            DateOnly? to,
            Domain.Entities.ClinicalRecord[] records)
        {
            return new ClinicalHistoryTemplateInformation
            {
                ConsultoryName = tenant.ConsultoryName,
                TenantIdentificationType = tenantIdentificationType,
                TenantIdentificationNumber = tenant.IdentificationNumber,
                TenantEmail = tenant.Email,
                TenantPhoneNumber = tenant.PhoneNumber,
                TenantAddress = tenant.Address,
                PatientIdentificationType = patientIdentificationType,
                PatientIdentificationNumber = patient.IdentificationNumber,
                PatientName = FormatGivenNames(patient.FirstName, patient.SecondName),
                PatientSurname = FormatGivenNames(patient.FirstSurname, patient.SecondSurname),
                PatientEmail = patient.Email,
                PatientPhoneNumber = patient.PhoneNumber,
                PatientBirthDate = patient.BirthDate,
                From = from,
                To = to,
                GeneratedAt = DateTimeHelper.ToColombiaTime(DateTime.UtcNow),
                Records = [.. records.Select(MapRecord)]
            };
        }

        private static ClinicalHistoryRecordInformation MapRecord(Domain.Entities.ClinicalRecord record)
        {
            return new ClinicalHistoryRecordInformation
            {
                RecordedAt = record.RecordedAt,
                Reason = record.Reason,
                Diagnosis = record.Diagnosis,
                Evolution = record.Evolution,
                Notes = record.Notes,
                CreatedByName = FormatGivenNames(
                    record.CreatedByUser.FirstName,
                    record.CreatedByUser.SecondName),
                CreatedBySurname = FormatGivenNames(
                    record.CreatedByUser.FirstSurname,
                    record.CreatedByUser.SecondSurname),
                AttendedByName = MapAttendedByName(record),
                AttendedBySurname = MapAttendedBySurname(record),
                Reference = MapReference(record),
                PatientTreatmentName = MapPatientTreatmentName(record),
                ProcedureName = MapProcedureName(record)
            };
        }

        private static string? MapAttendedByName(Domain.Entities.ClinicalRecord record)
        {
            var user = record.Appointment?.Professional?.User;
            if (user is null)
            {
                return null;
            }

            return FormatGivenNames(user.FirstName, user.SecondName);
        }

        private static string? MapAttendedBySurname(Domain.Entities.ClinicalRecord record)
        {
            var user = record.Appointment?.Professional?.User;
            if (user is null)
            {
                return null;
            }

            return FormatGivenNames(user.FirstSurname, user.SecondSurname);
        }

        private static string? MapReference(Domain.Entities.ClinicalRecord record)
        {
            if (record.Appointment is not null)
            {
                return "Cita";
            }

            if (record.PatientTreatment is not null)
            {
                return "Plan de tratamiento";
            }

            return null;
        }

        private static string? MapPatientTreatmentName(Domain.Entities.ClinicalRecord record)
        {
            var name = record.PatientTreatment?.Treatment?.Name
                ?? record.Appointment?.PatientTreatment?.Treatment?.Name;

            return string.IsNullOrWhiteSpace(name) ? null : name.Trim();
        }

        private static string? MapProcedureName(Domain.Entities.ClinicalRecord record)
        {
            if (record.Appointment?.AppointmentProcedures is { Count: > 0 })
            {
                var names = record.Appointment.AppointmentProcedures
                    .Select(p => p.Procedure?.Name)
                    .Where(name => !string.IsNullOrWhiteSpace(name))
                    .Select(name => name!.Trim())
                    .ToArray();

                if (names.Length > 0)
                {
                    return string.Join(", ", names);
                }
            }

            return string.IsNullOrWhiteSpace(record.Procedure?.Name)
                ? null
                : record.Procedure.Name.Trim();
        }

        private static string FormatGivenNames(string first, string? second)
        {
            return $"{first}{(!string.IsNullOrEmpty(second) ? $" {second}" : string.Empty)}";
        }

        private static DateTime? NormalizeFrom(DateOnly? from)
        {
            return from.HasValue
                ? from.Value.ToDateTime(TimeOnly.MinValue)
                : null;
        }

        private static DateTime? NormalizeToInclusive(DateOnly? to)
        {
            return to.HasValue
                ? to.Value.ToDateTime(TimeOnly.MinValue).AddDays(1).AddTicks(-1)
                : null;
        }

        private static string BuildFileName(Patient patient)
        {
            return $"historia-clinica-{patient.IdentificationNumber.Trim()}-{DateTime.UtcNow:yyyyMMdd}.pdf";
        }
    }
}
