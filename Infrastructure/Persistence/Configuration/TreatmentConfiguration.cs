using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class TreatmentConfiguration : IEntityTypeConfiguration<Treatment>
    {
        public void Configure(EntityTypeBuilder<Treatment> builder)
        {
            builder.ToTable("treatments");

            builder.HasKey(t => t.IdTreatment);

            builder.Property(t => t.IdTenant).IsRequired();

            builder.Property(t => t.Name).IsRequired().HasMaxLength(255);

            builder.Property(t => t.Description).HasMaxLength(500);

            builder.Property(t => t.IsActive).IsRequired();

            builder.Property(t => t.DeletedAt);

            builder.HasOne(t => t.Tenant)
                .WithMany(t => t.Treatments)
                .HasForeignKey(t => t.IdTenant)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(t => new { t.IdTenant, t.Name }).IsUnique();
        }
    }
}