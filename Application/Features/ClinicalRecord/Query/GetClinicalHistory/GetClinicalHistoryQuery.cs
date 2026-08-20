using Application.Common.Mediator.Interfaces;

namespace Application.Features.ClinicalRecord.Query.GetClinicalHistory
{
    public class GetClinicalHistoryQuery : IRequest<(byte[] Content, string FileName)>
    {
        public long IdPatient { get; set; }
        public DateOnly? From { get; set; }
        public DateOnly? To { get; set; }
    }
}
