using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    public class Payment : BaseEntity
    {
        [Column("id_payment")]
        public long IdPayment { get; set; }

        [Column("id_tenant")]
        public long IdTenant { get; set; }

        [Column("id_patient")]
        public long IdPatient { get; set; }

        [Column("id_appointment")]
        public long? IdAppointment { get; set; }

        [Column("id_patient_treatment")]
        public long? IdPatientTreatment { get; set; }

        [Column("amount")]
        public decimal Amount { get; set; }

        [Column("paid_at")]
        public DateTime PaidAt { get; set; }

        [Column("id_payment_method")]
        public short IdPaymentMethod { get; set; }

        [Column("notes")]
        public string? Notes { get; set; }

        public Tenant Tenant { get; set; } = null!;

        public Patient Patient { get; set; } = null!;

        public Appointment? Appointment { get; set; }

        public PatientTreatment? PatientTreatment { get; set; }

        public PaymentMethod PaymentMethod { get; set; } = null!;
    }
}