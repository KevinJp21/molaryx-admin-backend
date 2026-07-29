using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("permissions");

            builder.HasKey(p => p.IdPermission);

            builder.Property(p => p.IdModule).IsRequired();

            builder.Property(p => p.Code).IsRequired().HasMaxLength(50);

            builder.Property(p => p.CreatedAt).IsRequired();

            builder.Property(p => p.UpdatedAt);

            builder.HasOne(p => p.Module)
                .WithMany(m => m.Permissions)
                .HasForeignKey(p => p.IdModule)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasIndex(p => p.Code).IsUnique();
        }
    }
}