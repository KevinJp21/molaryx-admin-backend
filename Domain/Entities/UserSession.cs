using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class UserSession : BaseEntity
    {
        [Column("id_user_session")]
        public long IdUserSession { get; set; }

        [Column("id_user")]
        public long IdUser { get; set; }

        [Column("auth_token")]
        public string AuthToken { get; set; } = string.Empty;

        [Column("device")]
        public string? Device { get; set; }

        [Column("ip_connection")]
        public string? IpConnection { get; set; }

        // Navigation
        public User User { get; set; } = null!;
    }
}
