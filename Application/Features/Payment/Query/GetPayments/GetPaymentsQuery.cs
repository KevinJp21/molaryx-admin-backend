using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;

namespace Application.Features.Payment.Query.GetPayments
{
    public class GetPaymentsQuery : PageFilter, IRequest<PagedResult<GetPaymentsResponse>>
    {
        public long? IdPatient { get; set; }
        public long? IdAppointment { get; set; }
        public long? IdPatientTreatment { get; set; }
    }
}
