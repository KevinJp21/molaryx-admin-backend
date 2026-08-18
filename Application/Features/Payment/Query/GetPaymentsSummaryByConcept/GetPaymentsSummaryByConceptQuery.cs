using Application.Common.Mediator.Interfaces;

namespace Application.Features.Payment.Query.GetPaymentsSummaryByConcept
{
    public class GetPaymentsSummaryByConceptQuery : IRequest<GetPaymentsSummaryByConceptResponse>
    {
        public long? IdAppointment { get; set; }
        public long? IdPatientTreatment { get; set; }
    }
}
