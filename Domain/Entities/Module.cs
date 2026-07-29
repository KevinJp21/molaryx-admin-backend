using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    public class Module : BaseEntity
    {
        [Column("id_module")]
        public short IdModule { get; set; }
        [Column("code")]
        public string Code { get; set; } = string.Empty;

        public ICollection<Permission> Permissions { get; set; } = [];
    }
}