using Domain.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.IdUser);

            builder.Property(u => u.IdUserStatus).IsRequired();

            builder.Property(u => u.IdUserRole).IsRequired();

            builder.Property(u => u.IdTenant);

            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(FieldLengths.Username);

            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(FieldLengths.PersonName);

            builder.Property(u => u.SecondName)
                .HasMaxLength(FieldLengths.PersonName);

            builder.Property(u => u.FirstSurname)
                .IsRequired()
                .HasMaxLength(FieldLengths.PersonName);

            builder.Property(u => u.SecondSurname)
                .HasMaxLength(FieldLengths.PersonName);

            builder.Property(u => u.IdIdentificationType).IsRequired();

            builder.Property(u => u.IdentificationNumber)
                .IsRequired()
                .HasMaxLength(FieldLengths.IdentificationNumber);

            builder.Property(u => u.BirthDate).IsRequired();

            builder.Property(u => u.PhoneNumber)
                .IsRequired()
                .HasMaxLength(FieldLengths.PhoneNumber);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(FieldLengths.Email);

            builder.Property(u => u.Password).HasColumnType("bytea").IsRequired();

            builder.Property(u => u.Salt).HasColumnType("bytea").IsRequired();

            builder.Property(u => u.DeletedAt);

            builder.Property(u => u.CreatedAt).IsRequired();

            builder.Property(u => u.UpdatedAt);

            // Relaciones

            builder.HasOne(u => u.UserStatus)
                .WithMany()
                .HasForeignKey(u => u.IdUserStatus)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.UserRole)
                .WithMany()
                .HasForeignKey(u => u.IdUserRole)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.Tenant)
                .WithMany(t => t.Users)
                .HasForeignKey(u => u.IdTenant)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.IdentificationType)
                .WithMany()
                .HasForeignKey(u => u.IdIdentificationType)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(u => u.Username).IsUnique();

            builder.HasIndex(t => t.PhoneNumber).IsUnique();

            builder.HasIndex(t => t.Email).IsUnique();

            builder.HasIndex(u => u.IdentificationNumber).IsUnique();
        }
    }
}