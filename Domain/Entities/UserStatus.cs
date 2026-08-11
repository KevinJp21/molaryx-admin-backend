using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    public class UserStatus : BaseEntity
    {
        [Column("id_user_status")]
        public short IdUserStatus { get; set; }
        [Column("name")]
        public string Name { get; set; } = string.Empty;
    }
}