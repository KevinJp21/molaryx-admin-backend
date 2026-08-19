using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Specifications;

namespace Application.Features.PatientTreatment.Query.GetPatientTreatments
{
    public class GetPatientTreatmentsQueryHandler(
        IUnitOfWork _unitOfWork,
        ITenantAccessService _tenantAccessService
    ) : IRequestHandler<GetPatientTreatmentsQuery, PagedResult<GetPatientTreatmentsResponse>>
    {
        public async Task<PagedResult<GetPatientTreatmentsResponse>> Handle(
            GetPatientTreatmentsQuery request,
            CancellationToken cancellationToken = default)
        {
            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);

            var spec = new PatientTreatmentsSpec(
                access.IdTenant,
                request.IdPatient,
                request.Search,
                request.IdTreatmentStatus
            );

            var (totalItems, patientTreatments) = await _unitOfWork.PatientTreatmentRepository.GetPagedAsync(
                PaginationHelper.GetEffectivePage(request.Page),
                PaginationHelper.GetEffectivePageSize(request.Size),
                spec,
                cancellationToken
            );

            return new PagedResult<GetPatientTreatmentsResponse>
            {
                Items = [.. patientTreatments.Select(patientTreatment => new GetPatientTreatmentsResponse
                {
                    IdPatientTreatment = patientTreatment.IdPatientTreatment,
                    IdPatient = patientTreatment.IdPatient,
                    PatientName = $"{patientTreatment.Patient.FirstName}{(!string.IsNullOrEmpty(patientTreatment.Patient.SecondName) ? $" {patientTreatment.Patient.SecondName}" : string.Empty)}",
                    PatientSurname = $"{patientTreatment.Patient.FirstSurname}{(!string.IsNullOrEmpty(patientTreatment.Patient.SecondSurname) ? $" {patientTreatment.Patient.SecondSurname}" : string.Empty)}",
                    IdTreatment = patientTreatment.IdTreatment,
                    TreatmentName = patientTreatment.Treatment.Name,
                    AgreedPrice = patientTreatment.AgreedPrice,
                    IdPaymentFrequency = patientTreatment.IdPaymentFrequency,
                    PaymentFrequency = patientTreatment.PaymentFrequency?.Name,
                    PeriodicAmount = patientTreatment.PeriodicAmount,
                    StartAt = patientTreatment.StartAt,
                    EndAt = patientTreatment.EndAt,
                    IdTreatmentStatus = patientTreatment.IdTreatmentStatus,
                    TreatmentStatus = patientTreatment.TreatmentStatus.Name,
                    Notes = patientTreatment.Notes
                })],
                Page = PaginationHelper.GetEffectivePage(request.Page),
                Size = PaginationHelper.GetEffectivePageSize(request.Size),
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(
                    totalItems / (double)PaginationHelper.GetEffectivePageSize(request.Size)
                )
            };
        }
    }
}
