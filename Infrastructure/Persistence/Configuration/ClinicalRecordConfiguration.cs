using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class ClinicalRecordConfiguration : IEntityTypeConfiguration<ClinicalRecord>
    {
        public void Configure(EntityTypeBuilder<ClinicalRecord> builder)
        {
            builder.ToTable("clinical_records");
            builder.HasKey(c => c.IdClinicalRecord);
            builder.Property(c => c.IdTenant).IsRequired();
            builder.Property(c => c.IdPatient).IsRequired();
            builder.Property(c => c.IdAppointment);
            builder.Property(c => c.IdPatientTreatment);
            builder.Property(c => c.IdProcedure);
            builder.Property(c => c.IdCreatedByUser).IsRequired();
            builder.Property(c => c.RecordedAt).IsRequired();
            builder.Property(c => c.Reason).IsRequired().HasMaxLength(255);
            builder.Property(c => c.Diagnosis).HasMaxLength(1000);
            builder.Property(c => c.Evolution).HasMaxLength(2000);
            builder.Property(c => c.Notes).HasMaxLength(1000);

            builder.HasOne(c => c.Tenant)
                .WithMany(t => t.ClinicalRecords)
                .HasForeignKey(c => c.IdTenant)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Patient)
                .WithMany(p => p.ClinicalRecords)
                .HasForeignKey(c => new { c.IdTenant, c.IdPatient })
                .HasPrincipalKey(p => new { p.IdTenant, p.IdPatient })
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Appointment)
                .WithMany(a => a.ClinicalRecords)
                .HasForeignKey(c => new { c.IdTenant, c.IdAppointment })
                .HasPrincipalKey(a => new { a.IdTenant, a.IdAppointment })
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder.HasOne(c => c.PatientTreatment)
                .WithMany(p => p.ClinicalRecords)
                .HasForeignKey(c => new { c.IdTenant, c.IdPatientTreatment })
                .HasPrincipalKey(p => new { p.IdTenant, p.IdPatientTreatment })
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder.HasOne(c => c.Procedure)
                .WithMany(p => p.ClinicalRecords)
                .HasForeignKey(c => new { c.IdTenant, c.IdProcedure })
                .HasPrincipalKey(p => new { p.IdTenant, p.IdProcedure })
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder.HasOne(c => c.CreatedByUser)
                .WithMany(u => u.ClinicalRecords)
                .HasForeignKey(c => c.IdCreatedByUser)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
