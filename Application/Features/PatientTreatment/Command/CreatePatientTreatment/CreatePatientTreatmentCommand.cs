using Application.Common.Mediator.Interfaces;

namespace Application.Features.PatientTreatment.Command.CreatePatientTreatment
{
    public class CreatePatientTreatmentCommand : IRequest<bool>
    {
        public long IdPatient { get; set; }
        public long IdTreatment { get; set; }
        public decimal? AgreedPrice { get; set; }
        public short? IdPaymentFrequency { get; set; }
        public decimal? PeriodicAmount { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public string? Notes { get; set; }
    }
}