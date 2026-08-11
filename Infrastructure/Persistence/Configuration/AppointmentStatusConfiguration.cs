using Domain.Constants;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class AppointmentStatusConfiguration : IEntityTypeConfiguration<AppointmentStatus>
    {
        public void Configure(EntityTypeBuilder<AppointmentStatus> builder)
        {
            builder.ToTable("appointment_statuses");
            builder.HasKey(a => a.IdAppointmentStatus);
            builder.Property(a => a.Code).IsRequired().HasMaxLength(20);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(255);
            builder.Property(a => a.CreatedAt).IsRequired();
            builder.Property(a => a.UpdatedAt);

            builder.HasData(
                new AppointmentStatus
                {
                    IdAppointmentStatus = (short)AppointmentStatusEnum.PENDING,
                    Code = AppointmentStatusCodes.PENDING,
                    Name = "Pendiente",
                    CreatedAt = SeedConstants.SeedDate
                },
                new AppointmentStatus
                {
                    IdAppointmentStatus = (short)AppointmentStatusEnum.CONFIRMED,
                    Code = AppointmentStatusCodes.CONFIRMED,
                    Name = "Confirmado",
                    CreatedAt = SeedConstants.SeedDate
                },
                new AppointmentStatus
                {
                    IdAppointmentStatus = (short)AppointmentStatusEnum.IN_PROGRESS,
                    Code = AppointmentStatusCodes.IN_PROGRESS,
                    Name = "En progreso",
                    CreatedAt = SeedConstants.SeedDate
                },
                new AppointmentStatus
                {
                    IdAppointmentStatus = (short)AppointmentStatusEnum.COMPLETED,
                    Code = AppointmentStatusCodes.COMPLETED,
                    Name = "Completado",
                    CreatedAt = SeedConstants.SeedDate
                },
                new AppointmentStatus
                {
                    IdAppointmentStatus = (short)AppointmentStatusEnum.CANCELLED,
                    Code = AppointmentStatusCodes.CANCELLED,
                    Name = "Cancelado",
                    CreatedAt = SeedConstants.SeedDate
                },
                new AppointmentStatus
                {
                    IdAppointmentStatus = (short)AppointmentStatusEnum.NO_SHOW,
                    Code = AppointmentStatusCodes.NO_SHOW,
                    Name = "No se presentó",
                    CreatedAt = SeedConstants.SeedDate
                }
            );
        }
    }
}