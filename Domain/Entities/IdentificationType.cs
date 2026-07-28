using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class IdentificationType : BaseEntity
    {
        [Column("id_identification_type")]
        public short IdIdentificationType { get; set; }
        [Column("code")]
        public string Code { get; set; } = string.Empty;
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        // Navigation
        public ICollection<Tenant> Tenants { get; set; } = [];
        public ICollection<User> Users { get; set; } = [];
    }
}
