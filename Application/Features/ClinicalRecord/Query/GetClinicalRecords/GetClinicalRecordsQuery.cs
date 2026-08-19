using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;

namespace Application.Features.ClinicalRecord.Query.GetClinicalRecords
{
    public class GetClinicalRecordsQuery : PageFilter, IRequest<PagedResult<GetClinicalRecordsResponse>>
    {
        public long? IdPatient { get; set; }
        public long? IdAppointment { get; set; }
        public long? IdPatientTreatment { get; set; }
    }
}
