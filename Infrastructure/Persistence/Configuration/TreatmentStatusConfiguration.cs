using Domain.Entities;
using Domain.Enums;
using Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class TreatmentStatusConfiguration : IEntityTypeConfiguration<TreatmentStatus>
    {
        public void Configure(EntityTypeBuilder<TreatmentStatus> builder)
        {
            builder.ToTable("treatment_statuses");

            builder.HasKey(t => t.IdTreatmentStatus);

            builder.Property(t => t.Name).IsRequired().HasMaxLength(50);

            builder.Property(t => t.IsActive).IsRequired();

            builder.HasData(
                new TreatmentStatus
                {
                    IdTreatmentStatus = (short)TreatmentStatusEnum.ACTIVE,
                    Name = "Activo",
                    IsActive = true,
                    CreatedAt = SeedConstants.SeedDate
                },
                new TreatmentStatus
                {
                    IdTreatmentStatus = (short)TreatmentStatusEnum.PAUSED,
                    Name = "Pausado",
                    IsActive = true,
                    CreatedAt = SeedConstants.SeedDate
                },
                new TreatmentStatus
                {
                    IdTreatmentStatus = (short)TreatmentStatusEnum.COMPLETED,
                    Name = "Completado",
                    IsActive = true,
                    CreatedAt = SeedConstants.SeedDate
                },
                new TreatmentStatus
                {
                    IdTreatmentStatus = (short)TreatmentStatusEnum.CANCELLED,
                    Name = "Cancelado",
                    IsActive = true,
                    CreatedAt = SeedConstants.SeedDate
                }
            );
        }
    }
}
