using Application.Common.Mediator.Interfaces;

namespace Application.Features.ClinicalRecord.Query.GetClinicalHistory
{
    public class GetClinicalHistoryQuery : IRequest<ClinicalHistoryFileResult>
    {
        public long IdPatient { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
