using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class AppointmentProcedureConfiguration
        : IEntityTypeConfiguration<AppointmentProcedure>
    {
        public void Configure(EntityTypeBuilder<AppointmentProcedure> builder)
        {
            builder.ToTable("appointment_procedures");

            builder.HasKey(ap => ap.IdAppointmentProcedure);

            builder.Property(ap => ap.IdAppointment).IsRequired();
            builder.Property(ap => ap.IdProcedure).IsRequired();
            builder.Property(ap => ap.Price).IsRequired().HasPrecision(12, 2);
            builder.Property(ap => ap.Notes).HasMaxLength(500);

            builder.HasOne(ap => ap.Appointment)
                .WithMany(a => a.AppointmentProcedures)
                .HasForeignKey(ap => ap.IdAppointment)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ap => ap.Procedure)
                .WithMany(p => p.AppointmentProcedures)
                .HasForeignKey(ap => ap.IdProcedure)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(ap => new { ap.IdAppointment, ap.IdProcedure }).IsUnique();
        }
    }
}
