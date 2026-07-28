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

        [Column("refresh_token_hash")]
        public string? RefreshTokenHash { get; set; }

        [Column("expires_at")]
        public DateTime ExpiresAt { get; set; }

        [Column("revoked_at")]
        public DateTime? RevokedAt { get; set; }

        [Column("device")]
        public string? Device { get; set; }

        [Column("ip_connection")]
        public string? IpConnection { get; set; }

        [Column("last_login")]
        public DateTime? LastLogin { get; set; }

        // Navigation
        public User Users { get; set; } = null!;
    }
}
