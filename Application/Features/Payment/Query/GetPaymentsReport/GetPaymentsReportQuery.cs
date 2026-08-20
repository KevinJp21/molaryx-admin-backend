using Application.Common.Mediator.Interfaces;

namespace Application.Features.Payment.Query.GetPaymentsReport
{
    public class GetPaymentsReportQuery : IRequest<(byte[] Content, string FileName)>
    {
        public DateOnly? From { get; set; }
        public DateOnly? To { get; set; }
        public long? IdPatient { get; set; }
        public long? IdAppointment { get; set; }
        public long? IdPatientTreatment { get; set; }
    }
}
