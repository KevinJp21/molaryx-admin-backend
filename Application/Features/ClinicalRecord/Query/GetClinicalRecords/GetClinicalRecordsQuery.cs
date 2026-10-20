using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;

namespace Application.Features.ClinicalRecord.Query.GetClinicalRecords
{
    public class GetClinicalRecordsQuery : PageFilter, IRequest<PagedResult<GetClinicalRecordsResponse>>
    {
        public string? Search { get; set; }
        public DateOnly? From { get; set; }
        public DateOnly? To { get; set; }
    }
}
