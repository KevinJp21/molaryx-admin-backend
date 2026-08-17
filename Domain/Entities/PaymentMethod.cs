using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    public class PaymentMethod : BaseEntity
    {
        [Column("id_payment_method")]
        public short IdPaymentMethod { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("is_active")]
        public bool IsActive { get; set; }

        public ICollection<Payment> Payments { get; set; } = [];
    }
}