using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;

namespace Application.Features.PatientTreatment.Query.GetPatientTreatments
{
    public class GetPatientTreatmentsQuery : PageFilter, IRequest<PagedResult<GetPatientTreatmentsResponse>>
    {
        public long? IdPatient { get; set; }
        public short? IdTreatmentStatus { get; set; }
    }
}
