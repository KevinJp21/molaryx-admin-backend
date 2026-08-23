using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    public class AppointmentProcedure : BaseEntity
    {
        [Column("id_appointment_procedure")]
        public long IdAppointmentProcedure { get; set; }

        [Column("id_appointment")]
        public long IdAppointment { get; set; }

        [Column("id_procedure")]
        public long IdProcedure { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("notes")]
        public string? Notes { get; set; }

        public Appointment Appointment { get; set; } = null!;

        public Procedure Procedure { get; set; } = null!;
    }
}
