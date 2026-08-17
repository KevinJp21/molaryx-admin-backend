using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("payments", table =>
            {
                table.HasCheckConstraint(
                    "ck_payments_appointment_or_treatment",
                    """
                    (
                        id_appointment IS NOT NULL
                        AND id_patient_treatment IS NULL
                    )
                    OR
                    (
                        id_appointment IS NULL
                        AND id_patient_treatment IS NOT NULL
                    )
                    """);
            });

            builder.HasKey(p => p.IdPayment);

            builder.Property(p => p.IdTenant).IsRequired();
            builder.Property(p => p.IdPatient).IsRequired();

            builder.Property(p => p.IdAppointment);
            builder.Property(p => p.IdPatientTreatment);

            builder.Property(p => p.Amount)
                .HasPrecision(12, 2)
                .IsRequired();

            builder.Property(p => p.PaidAt)
                .IsRequired();

            builder.Property(p => p.IdPaymentMethod)
                .IsRequired();

            builder.Property(p => p.Notes)
                .HasMaxLength(500);

            builder.HasOne(p => p.Tenant)
                .WithMany(t => t.Payments)
                .HasForeignKey(p => p.IdTenant)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Patient)
                .WithMany(p => p.Payments)
                .HasForeignKey(p => new { p.IdTenant, p.IdPatient })
                .HasPrincipalKey(p => new { p.IdTenant, p.IdPatient })
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Appointment)
                .WithMany(a => a.Payments)
                .HasForeignKey(p => new { p.IdTenant, p.IdAppointment })
                .HasPrincipalKey(a => new { a.IdTenant, a.IdAppointment })
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.PatientTreatment)
                .WithMany(pt => pt.Payments)
                .HasForeignKey(p => p.IdPatientTreatment)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.PaymentMethod)
                .WithMany(m => m.Payments)
                .HasForeignKey(p => p.IdPaymentMethod)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}