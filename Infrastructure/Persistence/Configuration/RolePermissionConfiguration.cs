using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable("role_permissions");

            builder.HasKey(rp => new
            {
                rp.IdUserRole,
                rp.IdPermission
            });

            builder.HasOne(rp => rp.UserRole)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.IdUserRole)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.IdPermission)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}