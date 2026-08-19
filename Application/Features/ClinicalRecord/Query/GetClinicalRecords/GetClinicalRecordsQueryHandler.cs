using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Entities;
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

            var spec = ClinicalRecordSpec.ForList(
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
                Items = [.. clinicalRecords.Select(clinicalRecord => new GetClinicalRecordsResponse
                {
                    IdClinicalRecord = clinicalRecord.IdClinicalRecord,
                    IdPatient = clinicalRecord.IdPatient,
                    PatientName = FormatPersonName(clinicalRecord.Patient),
                    IdAppointment = clinicalRecord.IdAppointment,
                    IdPatientTreatment = clinicalRecord.IdPatientTreatment,
                    TreatmentName = clinicalRecord.PatientTreatment?.Treatment.Name,
                    IdService = clinicalRecord.IdService,
                    ServiceName = clinicalRecord.Service?.Name,
                    CreatedByUser = FormatPersonName(clinicalRecord.CreatedByUser),
                    RecordedAt = clinicalRecord.RecordedAt,
                    Reason = clinicalRecord.Reason,
                    Diagnosis = clinicalRecord.Diagnosis,
                    Evolution = clinicalRecord.Evolution,
                    Notes = clinicalRecord.Notes
                })],
                Page = PaginationHelper.GetEffectivePage(request.Page),
                Size = PaginationHelper.GetEffectivePageSize(request.Size),
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(
                    totalItems / (double)PaginationHelper.GetEffectivePageSize(request.Size)
                )
            };
        }

        private static string FormatPersonName(Patient patient)
            => FormatPersonName(
                patient.FirstName,
                patient.SecondName,
                patient.FirstSurname,
                patient.SecondSurname);

        private static string FormatPersonName(User user)
            => FormatPersonName(
                user.FirstName,
                user.SecondName,
                user.FirstSurname,
                user.SecondSurname);

        private static string FormatPersonName(
            string firstName,
            string? secondName,
            string firstSurname,
            string? secondSurname)
        {
            var given = $"{firstName}{(!string.IsNullOrEmpty(secondName) ? $" {secondName}" : string.Empty)}";
            var family = $"{firstSurname}{(!string.IsNullOrEmpty(secondSurname) ? $" {secondSurname}" : string.Empty)}";
            return $"{given} {family}".Trim();
        }
    }
}
