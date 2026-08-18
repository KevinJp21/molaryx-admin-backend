using Application.Common.Mediator.Interfaces;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Specifications;

namespace Application.Features.Payment.Query.GetPaymentsSummaryByConcept
{
    public class GetPaymentsSummaryByConceptQueryHandler(
        IUnitOfWork _unitOfWork,
        ITenantAccessService _tenantAccessService,
        ITenantResourceService _tenantResourceService
    ) : IRequestHandler<GetPaymentsSummaryByConceptQuery, GetPaymentsSummaryByConceptResponse>
    {
        public async Task<GetPaymentsSummaryByConceptResponse> Handle(
            GetPaymentsSummaryByConceptQuery request,
            CancellationToken cancellationToken = default)
        {
            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);

            decimal? price = null;
            decimal? agreedPrice = null;

            if (request.IdAppointment.GetValueOrDefault() > 0)
            {
                var appointment = await _tenantResourceService.RequireAppointmentAsync(
                    access.IdTenant,
                    request.IdAppointment!.Value,
                    cancellationToken);
                price = appointment.Price;
            }
            else
            {
                var patientTreatment = await _tenantResourceService.RequirePatientTreatmentAsync(
                    access.IdTenant,
                    request.IdPatientTreatment!.Value,
                    cancellationToken);
                agreedPrice = patientTreatment.AgreedPrice;
            }

            var payments = await _unitOfWork.PaymentRepository.GetAll(
                PaymentsSpec.ForConcept(
                    access.IdTenant,
                    request.IdAppointment,
                    request.IdPatientTreatment),
                cancellationToken) ?? [];

            var totalPaid = payments.Sum(payment => payment.Amount);
            var billed = price ?? agreedPrice;
            decimal? remaining = null;
            decimal? credit = null;

            if (billed is not null)
            {
                var difference = billed.Value - totalPaid;
                remaining = difference > 0 ? difference : 0;
                credit = difference < 0 ? -difference : 0;
            }

            return new GetPaymentsSummaryByConceptResponse
            {
                Price = price,
                AgreedPrice = agreedPrice,
                TotalPaid = totalPaid,
                Remaining = remaining,
                Credit = credit,
                PaymentCount = payments.Length
            };
        }
    }
}
