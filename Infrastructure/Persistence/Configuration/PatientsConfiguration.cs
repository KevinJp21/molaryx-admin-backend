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
            builder.Property(p => p.IdentificationNumber).IsRequired();
            builder.Property(p => p.FirstName).IsRequired();
            builder.Property(p => p.SecondName);
            builder.Property(p => p.FirstSurname).IsRequired();
            builder.Property(p => p.SecondSurname);
            builder.Property(p => p.BirthDate).IsRequired();
            builder.Property(p => p.PhoneNumber).IsRequired();
            builder.Property(p => p.Email).IsRequired();
            builder.Property(p => p.IsActive).IsRequired();

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
        }
    }
}