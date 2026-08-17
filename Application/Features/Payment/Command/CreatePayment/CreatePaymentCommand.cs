using Application.Common.Mediator.Interfaces;

namespace Application.Features.Payment.Command.CreatePayment
{
    public class CreatePaymentCommand : IRequest<bool>
    {
        public long IdPatient { get; set; }
        public long? IdAppointment { get; set; }
        public long? IdPatientTreatment { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaidAt { get; set; }
        public short IdPaymentMethod { get; set; }
        public string? Notes { get; set; }
    }
}
