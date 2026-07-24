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

            builder.Property(u => u.IdUserRol).IsRequired();

            builder.Property(u => u.Username).IsRequired();

            builder.Property(u => u.FirstName).IsRequired();

            builder.Property(u => u.SecondName);

            builder.Property(u => u.FirstSurname).IsRequired();

            builder.Property(u => u.SecondSurname);

            builder.Property(u => u.IdIdentificationType).IsRequired();

            builder.Property(u => u.IdentificationNumber).IsRequired();

            builder.Property(u => u.Email).IsRequired();

            builder.Property(u => u.Password).HasColumnType("bytea").IsRequired();

            builder.Property(u => u.Salt).HasColumnType("bytea").IsRequired();

            builder.Property(t => t.CreatedAt).IsRequired();

            builder.Property(t => t.UpdatedAt);

            // Relaciones

            builder.HasOne(u => u.UserStatus)
                .WithMany(s => s.User)
                .HasForeignKey(u => u.IdUserStatus)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.UserRole)
                .WithMany(ur => ur.User)
                .HasForeignKey(u => u.IdUserRol)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.IdentificationType)
                .WithMany(i => i.User)
                .HasForeignKey(u => u.IdIdentificationType)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(t => t.Email).IsUnique();
        }
    }
}