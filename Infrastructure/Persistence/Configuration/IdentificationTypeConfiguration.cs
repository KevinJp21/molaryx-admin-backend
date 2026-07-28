using Domain.Constants;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class IdentificationTypeConfiguration : IEntityTypeConfiguration<IdentificationType>
    {
        public void Configure(EntityTypeBuilder<IdentificationType> builder)
        {
            builder.ToTable("identification_types");

            builder.HasKey(i => i.IdIdentificationType);

            builder.Property(i => i.Code).IsRequired().HasMaxLength(5);

            builder.Property(i => i.Name).IsRequired().HasMaxLength(255);

            builder.Property(i => i.CreatedAt).IsRequired();

            builder.Property(i => i.UpdatedAt);
            
            builder.HasData(
                new IdentificationType
                {
                    IdIdentificationType = (short)IdentificationTypeEnum.CC,
                    Code = IdentificationTypeCode.CC,
                    Name = "Cédula de ciudadanía",
                    CreatedAt = SeedConstants.SeedDate
                },
                new IdentificationType
                {
                    IdIdentificationType = (short)IdentificationTypeEnum.CE,
                    Code = IdentificationTypeCode.CE,
                    Name = "Cédula de extranjería",
                    CreatedAt = SeedConstants.SeedDate
                },
                new IdentificationType
                {
                    IdIdentificationType = (short)IdentificationTypeEnum.TI,
                    Code = IdentificationTypeCode.TI,
                    Name = "Tarjeta de identidad",
                    CreatedAt = SeedConstants.SeedDate
                },
                new IdentificationType
                {
                    IdIdentificationType = (short)IdentificationTypeEnum.NIT,
                    Code = IdentificationTypeCode.NIT,
                    Name = "Número de Identificación Tributaria",
                    CreatedAt = SeedConstants.SeedDate
                }
            );
        }
    }
}