using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Specifications;

namespace Application.Features.Payment.Query.GetPayments
{
    public class GetPaymentsQueryHandler(
        IUnitOfWork _unitOfWork,
        ITenantAccessService _tenantAccessService
    ) : IRequestHandler<GetPaymentsQuery, PagedResult<GetPaymentsResponse>>
    {
        public async Task<PagedResult<GetPaymentsResponse>> Handle(
            GetPaymentsQuery request,
            CancellationToken cancellationToken = default)
        {
            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);

            var spec = new PaymentsSpec(
                access.IdTenant,
                request.IdPatient,
                request.IdAppointment,
                request.IdPatientTreatment);

            var (totalItems, payments) = await _unitOfWork.PaymentRepository.GetPagedAsync(
                PaginationHelper.GetEffectivePage(request.Page),
                PaginationHelper.GetEffectivePageSize(request.Size),
                spec,
                cancellationToken
            );

            return new PagedResult<GetPaymentsResponse>
            {
                Items = [.. payments.Select(payment => new GetPaymentsResponse
                {
                    IdPayment = payment.IdPayment,
                    IdPatient = payment.IdPatient,
                    IdAppointment = payment.IdAppointment,
                    IdPatientTreatment = payment.IdPatientTreatment,
                    Amount = payment.Amount,
                    PaidAt = payment.PaidAt,
                    IdPaymentMethod = payment.IdPaymentMethod,
                    PaymentMethod = payment.PaymentMethod.Name,
                    Notes = payment.Notes
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
