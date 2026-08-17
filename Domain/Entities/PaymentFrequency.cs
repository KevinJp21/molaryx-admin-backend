using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    public class PaymentFrequency : BaseEntity
    {
        [Column("id_payment_frequency")]
        public short IdPaymentFrequency { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("is_active")]
        public bool IsActive { get; set; }

        public ICollection<PatientTreatment> PatientTreatments { get; set; } = [];
    }
}