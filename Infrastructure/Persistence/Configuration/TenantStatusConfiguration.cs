using Domain.Entities;
using Domain.Enums;
using Infrastructure.Constants;
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
            
            builder.HasData(
                new TenantStatus
                {
                    IdTenantStatus = (short)TenantStatusEnum.ACTIVO,
                    Name = "Activo",
                    CreatedAt = SeedConstants.SeedDate
                },
                new TenantStatus
                {
                    IdTenantStatus = (short)TenantStatusEnum.INACTIVO,
                    Name = "Inactivo",
                    CreatedAt = SeedConstants.SeedDate
                },
                new TenantStatus
                {
                    IdTenantStatus = (short)TenantStatusEnum.HABILITACION_PENDIENTE,
                    Name = "Habilitación pendiente",
                    CreatedAt = SeedConstants.SeedDate
                },
                new TenantStatus
                {
                    IdTenantStatus = (short)TenantStatusEnum.BLOQUEADO,
                    Name = "Bloqueado",
                    CreatedAt = SeedConstants.SeedDate
                }
            );
        }
    }
}