using Domain.Constants;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class TenantTypeConfiguration : IEntityTypeConfiguration<TenantType>
    {
        public void Configure(EntityTypeBuilder<TenantType> builder)
        {
            builder.ToTable("tenant_types");

            builder.HasKey(t => t.IdTenantType);

            builder.Property(t => t.Code).IsRequired().HasMaxLength(50);

            builder.Property(t => t.CreatedAt).IsRequired();

            builder.Property(t => t.UpdatedAt);

            builder.HasData(
                new TenantType
                {
                    IdTenantType = (short)TenantTypeEnum.STANDARD,
                    Code = TenantTypeCodes.STANDARD,
                    CreatedAt = SeedConstants.SeedDate
                },
                new TenantType
                {
                    IdTenantType = (short)TenantTypeEnum.FOUNDER,
                    Code = TenantTypeCodes.FOUNDER,
                    CreatedAt = SeedConstants.SeedDate
                }
            );
            
            builder.HasIndex(t => t.Code).IsUnique();
        }
    }
}