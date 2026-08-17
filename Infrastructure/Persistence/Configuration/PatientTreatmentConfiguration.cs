using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class PatientTreatmentConfiguration : IEntityTypeConfiguration<PatientTreatment>
    {
        public void Configure(EntityTypeBuilder<PatientTreatment> builder)
        {
            builder.ToTable("patient_treatments");

            builder.HasKey(p => p.IdPatientTreatment);

            builder.Property(p => p.IdTenant).IsRequired();
            builder.Property(p => p.IdPatient).IsRequired();
            builder.Property(p => p.IdTreatment).IsRequired();

            builder.Property(p => p.AgreedPrice)
                .HasPrecision(12, 2);

            builder.Property(p => p.PeriodicAmount)
                .HasPrecision(12, 2);

            builder.Property(p => p.StartAt).IsRequired();
            builder.Property(p => p.EndAt);

            builder.Property(p => p.IdTreatmentStatus).IsRequired();

            builder.Property(p => p.Notes)
                .HasMaxLength(500);

            builder.HasOne(p => p.Tenant)
                .WithMany(t => t.PatientTreatments)
                .HasForeignKey(p => p.IdTenant)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Patient)
                .WithMany(p => p.PatientTreatments)
                .HasForeignKey(p => new { p.IdTenant, p.IdPatient })
                .HasPrincipalKey(p => new { p.IdTenant, p.IdPatient })
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Treatment)
                .WithMany(t => t.PatientTreatments)
                .HasForeignKey(p => new { p.IdTenant, p.IdTreatment })
                .HasPrincipalKey(t => new { t.IdTenant, t.IdTreatment })
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.PaymentFrequency)
                .WithMany(f => f.PatientTreatments)
                .HasForeignKey(p => p.IdPaymentFrequency)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.TreatmentStatus)
                .WithMany(s => s.PatientTreatments)
                .HasForeignKey(p => p.IdTreatmentStatus)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
