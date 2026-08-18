using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    public class Appointment : BaseEntity
    {
        [Column("id_appointment")]
        public long IdAppointment { get; set; }

        [Column("id_tenant")]
        public long IdTenant { get; set; }

        [Column("id_patient")]
        public long IdPatient { get; set; }

        [Column("id_professional")]
        public long IdProfessional { get; set; }

        [Column("id_service")]
        public long IdService { get; set; }

        [Column("id_patient_treatment")]
        public long? IdPatientTreatment { get; set; }

        [Column("price")]
        public decimal? Price { get; set; }

        [Column("id_appointment_status")]
        public short IdAppointmentStatus { get; set; }

        [Column("start_at")]
        public DateTime StartAt { get; set; }

        [Column("end_at")]
        public DateTime EndAt { get; set; }

        [Column("notes")]
        public string? Notes { get; set; }

        public Tenant Tenant { get; set; } = null!;

        public Patient Patient { get; set; } = null!;

        public Professional Professional { get; set; } = null!;

        public Service Service { get; set; } = null!;

        public PatientTreatment? PatientTreatment { get; set; }

        public AppointmentStatus AppointmentStatus { get; set; } = null!;

        public ICollection<Payment> Payments { get; set; } = [];
        public ICollection<ClinicalRecord> ClinicalRecords { get; set; } = [];
    }
}