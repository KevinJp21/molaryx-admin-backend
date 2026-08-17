using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    public class PatientTreatment : BaseEntity
    {
        [Column("id_patient_treatment")]
        public long IdPatientTreatment { get; set; }

        [Column("id_tenant")]
        public long IdTenant { get; set; }

        [Column("id_patient")]
        public long IdPatient { get; set; }

        [Column("id_treatment")]
        public long IdTreatment { get; set; }

        [Column("agreed_price")]
        public decimal? AgreedPrice { get; set; }

        [Column("id_payment_frequency")]
        public short? IdPaymentFrequency { get; set; }

        [Column("periodic_amount")]
        public decimal? PeriodicAmount { get; set; }

        [Column("start_at")]
        public DateTime StartAt { get; set; }

        [Column("end_at")]
        public DateTime? EndAt { get; set; }

        [Column("id_treatment_status")]
        public short IdTreatmentStatus { get; set; }

        [Column("notes")]
        public string? Notes { get; set; }


        public Tenant Tenant { get; set; } = null!;

        public Patient Patient { get; set; } = null!;

        public Treatment Treatment { get; set; } = null!;

        public PaymentFrequency? PaymentFrequency { get; set; }

        public TreatmentStatus TreatmentStatus { get; set; } = null!;

        public ICollection<Appointment> Appointments { get; set; } = [];

        public ICollection<Payment> Payments { get; set; } = [];
    }
}