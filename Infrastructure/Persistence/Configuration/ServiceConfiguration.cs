using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("services");

            builder.HasKey(s => s.IdService);
            builder.Property(s => s.IdTenant).IsRequired();
            builder.Property(s => s.Name).IsRequired();
            builder.Property(s => s.Description);
            builder.Property(s => s.IsActive).IsRequired();
            builder.Property(s => s.DeletedAt);

            builder.HasOne(s => s.Tenant)
                .WithMany(t => t.Services)
                .HasForeignKey(s => s.IdTenant)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(s => new { s.IdTenant, s.IdService }).IsUnique();
            builder.HasIndex(s => new { s.IdTenant, s.Name })
                .IsUnique()
                .HasFilter("deleted_at IS NULL");
        }
    }
}