using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class TenantStatusConfiguration : IEntityTypeConfiguration<TenantStatus>
    {
        public void Configure(EntityTypeBuilder<TenantStatus> builder)
        {
            builder.ToTable("tenant_statuses");

            builder.HasKey(s => s.IdTenantStatus);

            builder.Property(s => s.IdTenantStatus).ValueGeneratedOnAdd();

            builder.Property(s => s.Code).IsRequired();

            builder.Property(s => s.Name).IsRequired().HasMaxLength(255);

            builder.Property(s => s.CreatedAt).IsRequired();

            builder.Property(s => s.UpdatedAt);

            builder.HasIndex(s => s.Code).IsUnique();
        }
    }
}