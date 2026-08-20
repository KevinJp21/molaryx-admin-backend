using Application.Common.Mediator.Interfaces;

namespace Application.Features.Payment.Query.GetPaymentReport
{
    public class GetPaymentReportQuery : IRequest<PaymentReportFileResult>
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public long? IdPatient { get; set; }
        public long? IdAppointment { get; set; }
        public long? IdPatientTreatment { get; set; }
    }
}
