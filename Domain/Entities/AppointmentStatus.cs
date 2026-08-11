using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    public class AppointmentStatus : BaseEntity
    {
        [Column("id_appointment_status")]
        public short IdAppointmentStatus { get; set; }
        [Column("code")]
        public string Code { get; set; } = string.Empty;
        [Column("name")]
        public string Name { get; set; } = string.Empty;
    }
}