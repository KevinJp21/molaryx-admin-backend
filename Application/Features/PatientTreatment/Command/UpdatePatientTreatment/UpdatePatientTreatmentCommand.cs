using Application.Common.Mediator.Interfaces;

namespace Application.Features.PatientTreatment.Command.UpdatePatientTreatment
{
    public class UpdatePatientTreatmentCommand : IRequest<bool>
    {
        public long IdPatientTreatment { get; set; }
        public decimal? AgreedPrice { get; set; }
        public short? IdPaymentFrequency { get; set; }
        public decimal? PeriodicAmount { get; set; }
        public DateTime? StartAt { get; set; }
        public short? IdTreatmentStatus { get; set; }
        public string? Notes { get; set; }
    }
}
