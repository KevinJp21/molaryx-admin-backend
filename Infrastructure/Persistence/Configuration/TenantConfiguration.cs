using Domain.Constants;
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

            builder.Property(t => t.IdentificationNumber)
                .HasMaxLength(FieldLengths.IdentificationNumber);

            builder.Property(t => t.ConsultoryName)
                .IsRequired()
                .HasMaxLength(FieldLengths.ConsultoryName);

            builder.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(FieldLengths.Email);

            builder.Property(t => t.PhoneNumber)
                .IsRequired()
                .HasMaxLength(FieldLengths.PhoneNumber);

            builder.Property(t => t.Address)
                .IsRequired()
                .HasMaxLength(FieldLengths.Address);

            builder.Property(t => t.CreatedAt).IsRequired();

            builder.Property(t => t.UpdatedAt);

            // Relaciones

            builder.HasOne(t => t.TenantType)
                .WithMany()
                .HasForeignKey(t => t.IdTenantType)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.TenantStatus)
                .WithMany()
                .HasForeignKey(t => t.IdTenantStatus)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.IdentificationType)
                .WithMany()
                .HasForeignKey(t => t.IdIdentificationType)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(t => t.IdentificationNumber).IsUnique();

            builder.HasIndex(t => t.Email).IsUnique();

            builder.HasIndex(t => t.PhoneNumber).IsUnique();
        }
    }
}