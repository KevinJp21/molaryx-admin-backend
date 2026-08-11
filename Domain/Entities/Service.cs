using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    public class Service : BaseEntity
{
    [Column("id_service")]
    public long IdService { get; set; }

    [Column("id_tenant")]
    public long IdTenant { get; set; }

    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("description")]
    public string? Description { get; set; }

    [Column("price")]
    public decimal Price { get; set; }

    [Column("duration_minutes")]
    public short DurationMinutes { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    public Tenant Tenant { get; set; } = null!;

    public ICollection<Appointment> Appointments { get; set; } = [];
}
}