using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.ToTable("tenants");

            builder.HasKey(t => t.IdTenant);

            builder.Property(t => t.IdTenantType).IsRequired();

            builder.Property(t => t.IdTenantStatus).IsRequired();

            builder.Property(t => t.IdIdentificationType);

            builder.Property(t => t.IdentificationNumber).HasMaxLength(15);

            builder.Property(t => t.ConsultoryName).IsRequired().HasMaxLength(255);

            builder.Property(t => t.Email).IsRequired();

            builder.Property(t => t.PhoneNumber).IsRequired();

            builder.Property(t => t.Address).IsRequired();

            builder.Property(t => t.CreatedAt).IsRequired();

            builder.Property(t => t.UpdatedAt);

            // Relaciones

            builder.HasOne(t => t.TenantType)
                .WithMany(tt => tt.Tenants)
                .HasForeignKey(t => t.IdTenantType)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.TenantStatus)
                .WithMany(s => s.Tenants)
                .HasForeignKey(t => t.IdTenantStatus)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.IdentificationType)
                .WithMany(it => it.Tenants)
                .HasForeignKey(t => t.IdIdentificationType)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(t => t.IdentificationNumber).IsUnique();

            builder.HasIndex(t => t.Email).IsUnique();

            builder.HasIndex(t => t.PhoneNumber).IsUnique();
        }
    }
}