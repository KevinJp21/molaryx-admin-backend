using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common
{
    public class BaseEntity
    {
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        protected BaseEntity()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = null;
        }
    }
}