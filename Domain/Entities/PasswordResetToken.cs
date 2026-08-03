using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    public class PasswordResetToken : BaseEntity
    {
        [Column("id_password_reset_token")]
        public long IdPasswordResetToken { get; set; }

        [Column("id_user")]
        public long IdUser { get; set; }

        [Column("token")]
        public string Token { get; set; } = string.Empty;

        [Column("expires_at")]
        public DateTime ExpiresAt { get; set; }

        [Column("used_at")]
        public DateTime? UsedAt { get; set; }

        // Navigation
        public User User { get; set; } = null!;
    }
}