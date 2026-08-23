using Application.Common.Mediator.Interfaces;

namespace Application.Features.ClinicalRecord.Command.CreateClinicalRecord
{
    public class CreateClinicalRecordCommand : IRequest<bool>
    {
        public long IdPatient { get; set; }
        public long? IdAppointment { get; set; }
        public long? IdPatientTreatment { get; set; }
        public long? IdProcedure { get; set; }

        public DateTime RecordedAt { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string? Diagnosis { get; set; }
        public string? Evolution { get; set; }
        public string? Notes { get; set; }
    }
}