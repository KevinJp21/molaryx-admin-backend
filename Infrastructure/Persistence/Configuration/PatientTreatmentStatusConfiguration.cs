using Domain.Entities;
using Domain.Enums;
using Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class PatientTreatmentStatusConfiguration : IEntityTypeConfiguration<PatientTreatmentStatus>
    {
        public void Configure(EntityTypeBuilder<PatientTreatmentStatus> builder)
        {
            builder.ToTable("patient_treatment_statuses");

            builder.HasKey(t => t.IdPatientTreatmentStatus);

            builder.Property(t => t.Name).IsRequired().HasMaxLength(50);

            builder.Property(t => t.IsActive).IsRequired();

            builder.HasData(
                new PatientTreatmentStatus
                {
                    IdPatientTreatmentStatus = (short)PatientTreatmentStatusEnum.ACTIVE,
                    Name = "Activo",
                    IsActive = true,
                    CreatedAt = SeedConstants.SeedDate
                },
                new PatientTreatmentStatus
                {
                    IdPatientTreatmentStatus = (short)PatientTreatmentStatusEnum.PAUSED,
                    Name = "Pausado",
                    IsActive = true,
                    CreatedAt = SeedConstants.SeedDate
                },
                new PatientTreatmentStatus
                {
                    IdPatientTreatmentStatus = (short)PatientTreatmentStatusEnum.COMPLETED,
                    Name = "Completado",
                    IsActive = true,
                    CreatedAt = SeedConstants.SeedDate
                },
                new PatientTreatmentStatus
                {
                    IdPatientTreatmentStatus = (short)PatientTreatmentStatusEnum.CANCELLED,
                    Name = "Cancelado",
                    IsActive = true,
                    CreatedAt = SeedConstants.SeedDate
                }
            );
        }
    }
}
