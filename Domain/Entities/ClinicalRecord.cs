using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    public class ClinicalRecord : BaseEntity
    {
        [Column("id_clinical_record")]
        public long IdClinicalRecord { get; set; }

        [Column("id_tenant")]
        public long IdTenant { get; set; }

        [Column("id_patient")]
        public long IdPatient { get; set; }

        [Column("id_appointment")]
        public long? IdAppointment { get; set; }

        [Column("id_patient_treatment")]
        public long? IdPatientTreatment { get; set; }

        [Column("id_procedure")]
        public long? IdProcedure { get; set; }

        [Column("id_created_by_user")]
        public long IdCreatedByUser { get; set; }

        [Column("recorded_at")]
        public DateTime RecordedAt { get; set; }

        [Column("reason")]
        public string Reason { get; set; } = string.Empty;

        [Column("diagnosis")]
        public string? Diagnosis { get; set; }

        [Column("evolution")]
        public string? Evolution { get; set; }

        [Column("notes")]
        public string? Notes { get; set; }

        public Tenant Tenant { get; set; } = null!;

        public Patient Patient { get; set; } = null!;

        public Appointment? Appointment { get; set; }

        public PatientTreatment? PatientTreatment { get; set; }

        public Procedure? Procedure { get; set; }

        public User CreatedByUser { get; set; } = null!;
    }
}
