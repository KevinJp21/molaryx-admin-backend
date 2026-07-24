using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    public class UserRole : BaseEntity
    {
        [Column("id_user_role")]
        public short IdUserRole { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("description")]
        public string? Description { get; set; }

        public ICollection<User> User { get; set; } = [];
    }
}