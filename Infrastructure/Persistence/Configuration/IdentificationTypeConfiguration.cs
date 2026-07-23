using Domain.Entities;
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

            var currentDate = new DateTime(
                2026, 7, 22,
                0, 0, 0,
                DateTimeKind.Utc
            );
            
            builder.HasData(
                new IdentificationType
                {
                    IdIdentificationType = 1,
                    Code = "CC",
                    Name = "Cédula de ciudadanía",
                    CreatedAt = currentDate,
                    UpdatedAt = null
                },
                new IdentificationType
                {
                    IdIdentificationType = 2,
                    Code = "CI",
                    Name = "Cédula de extranjería",
                    CreatedAt = currentDate,
                    UpdatedAt = null
                },
                new IdentificationType
                {
                    IdIdentificationType = 3,
                    Code = "TI",
                    Name = "Tarjeta de identidad",
                    CreatedAt = currentDate,
                    UpdatedAt = null
                }
            );
        }
    }
}