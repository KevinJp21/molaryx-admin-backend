using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class ProfessionalSchedule : BaseEntity
    {
        [Column("id_professional_schedule")]
        public long IdProfessionalSchedule { get; set; }

        [Column("id_tenant")]
        public long IdTenant { get; set; }

        [Column("id_professional")]
        public long IdProfessional { get; set; }

        [Column("day_of_week")]
        public DayOfWeekEnum DayOfWeek { get; set; }

        [Column("start_time")]
        public TimeSpan StartTime { get; set; }

        [Column("end_time")]
        public TimeSpan EndTime { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        public Tenant Tenant { get; set; } = null!;
        public Professional Professional { get; set; } = null!;
    }
}
