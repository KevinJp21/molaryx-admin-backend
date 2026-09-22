using Domain.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("patients");

            builder.HasKey(p => p.IdPatient);

            builder.Property(p => p.IdTenant).IsRequired();
            builder.Property(p => p.IdIdentificationType).IsRequired();
            builder.Property(p => p.IdentificationNumber)
                .IsRequired()
                .HasMaxLength(FieldLengths.IdentificationNumber);
            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(FieldLengths.PersonName);
            builder.Property(p => p.SecondName)
                .HasMaxLength(FieldLengths.PersonName);
            builder.Property(p => p.FirstSurname)
                .IsRequired()
                .HasMaxLength(FieldLengths.PersonName);
            builder.Property(p => p.SecondSurname)
                .HasMaxLength(FieldLengths.PersonName);
            builder.Property(p => p.BirthDate).IsRequired();
            builder.Property(p => p.PhoneNumber)
                .IsRequired()
                .HasMaxLength(FieldLengths.PhoneNumber);
            builder.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(FieldLengths.Email);
            builder.Property(p => p.IsActive).IsRequired();
            builder.Property(p => p.DeletedAt);

            builder.HasOne(p => p.Tenant)
                .WithMany(t => t.Patients)
                .HasForeignKey(p => p.IdTenant)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.IdentificationType)
                .WithMany()
                .HasForeignKey(p => p.IdIdentificationType)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(p => new
            {
                p.IdTenant,
                p.IdentificationNumber
            }).IsUnique();

            builder.HasIndex(p => new { p.IdTenant, p.Email }).IsUnique();
            builder.HasIndex(p => new { p.IdTenant, p.PhoneNumber }).IsUnique();
            builder.HasIndex(p => new { p.IdTenant, p.IdPatient }).IsUnique();

            builder.HasIndex(p => p.FirstName)
                .HasDatabaseName("ix_patients_first_name_trgm")
                .HasMethod("gin")
                .HasOperators("gin_trgm_ops");

            builder.HasIndex(p => p.FirstSurname)
                .HasDatabaseName("ix_patients_first_surname_trgm")
                .HasMethod("gin")
                .HasOperators("gin_trgm_ops");

            builder.HasIndex(p => p.IdentificationNumber)
                .HasDatabaseName("ix_patients_identification_number_trgm")
                .HasMethod("gin")
                .HasOperators("gin_trgm_ops");
        }
    }
}