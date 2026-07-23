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

            builder.Property(s => s.Name).IsRequired().HasMaxLength(255);

            builder.Property(s => s.CreatedAt).IsRequired();

            builder.Property(s => s.UpdatedAt);

            var currentDate = new DateTime(
                2026, 7, 22,
                0, 0, 0,
                DateTimeKind.Utc
            );
            
            builder.HasData(
                new TenantStatus
                {
                    IdTenantStatus = 1,
                    Name = "Activo",
                    CreatedAt = currentDate,
                    UpdatedAt = null
                },
                new TenantStatus
                {
                    IdTenantStatus = 2,
                    Name = "Inactivo",
                    CreatedAt = currentDate,
                    UpdatedAt = null
                },
                new TenantStatus
                {
                    IdTenantStatus = 3,
                    Name = "Habilitación pendiente",
                    CreatedAt = currentDate,
                    UpdatedAt = null
                },
                new TenantStatus
                {
                    IdTenantStatus = 4,
                    Name = "Bloqueado",
                    CreatedAt = currentDate,
                    UpdatedAt = null
                }
            );
        }
    }
}