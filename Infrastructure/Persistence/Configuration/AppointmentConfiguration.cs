using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class AppointmentConfiguration
        : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("appointments", table =>
            {
                table.HasCheckConstraint(
                    "ck_appointments_treatment_or_price",
                    """
                    (
                        id_patient_treatment IS NULL
                        OR price IS NULL
                    )
                    """);
            });

            builder.HasKey(a => a.IdAppointment);

            builder.Property(a => a.IdAppointment).IsRequired();
            builder.Property(a => a.IdTenant).IsRequired();
            builder.Property(a => a.IdPatient).IsRequired();
            builder.Property(a => a.IdProfessional).IsRequired();
            builder.Property(a => a.IdService).IsRequired();
            builder.Property(a => a.IdPatientTreatment);
            builder.Property(a => a.Price).HasPrecision(12, 2);
            builder.Property(a => a.IdAppointmentStatus).IsRequired();
            builder.Property(a => a.StartAt).IsRequired();
            builder.Property(a => a.EndAt).IsRequired();
            builder.Property(a => a.Notes);

            builder.HasOne(a => a.Tenant)
                .WithMany(t => t.Appointments)
                .HasForeignKey(a => a.IdTenant)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => new { a.IdTenant, a.IdPatient })
                .HasPrincipalKey(p => new { p.IdTenant, p.IdPatient })
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Professional)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => new { a.IdTenant, a.IdProfessional })
                .HasPrincipalKey(p => new { p.IdTenant, p.IdProfessional })
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Service)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => new { a.IdTenant, a.IdService })
                .HasPrincipalKey(s => new { s.IdTenant, s.IdService })
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.AppointmentStatus)
                .WithMany()
                .HasForeignKey(a => a.IdAppointmentStatus)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.PatientTreatment)
                .WithMany(pt => pt.Appointments)
                .HasForeignKey(a => a.IdPatientTreatment)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder.HasIndex(a => new { a.IdTenant, a.IdAppointment }).IsUnique();
        }
    }
}