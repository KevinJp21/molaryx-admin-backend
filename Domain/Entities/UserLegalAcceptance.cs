using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    public class UserLegalAcceptance : BaseEntity
    {
        [Column("id_user_legal_acceptance")]
        public long IdUserLegalAcceptance { get; set; }

        [Column("id_user")]
        public long IdUser { get; set; }

        [Column("id_document_type")]
        public short IdDocumentType { get; set; }

        [Column("document_version")]
        public string DocumentVersion { get; set; } = string.Empty;

        [Column("accepted_at")]
        public DateTime AcceptedAt { get; set; }

        [Column("ip_address")]
        public string? IpAddress { get; set; }

        [Column("user_agent")]
        public string? UserAgent { get; set; }

        public User User { get; set; } = null!;
    }
}
