using Domain.Entities;
using Domain.Enums;
using Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("user_roles");

            builder.HasKey(ur => ur.IdUserRole);

            builder.Property(ur => ur.Code)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(ur => ur.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(ur => ur.Description)
                .HasMaxLength(255);

            builder.Property(t => t.CreatedAt).IsRequired();

            builder.Property(t => t.UpdatedAt);

            builder.HasIndex(ur => ur.Name).IsUnique();

            builder.HasData(
                new UserRole
                {
                    IdUserRole = (short)UserRoleEnum.SUPER_ADMIN,
                    Code = "SUPER_ADMIN",
                    Name = "Super Administrador",
                    Description = "Acceso completo a la plataforma y administración global de todos los consultorios.",
                    CreatedAt = SeedConstants.SeedDate
                },
                new UserRole
                {
                    IdUserRole = (short)UserRoleEnum.OWNER,
                    Code = "OWNER",
                    Name = "Propietario",
                    Description = "Acceso completo a la gestión de su consultorio y sus operaciones.",
                    CreatedAt = SeedConstants.SeedDate
                },
                new UserRole
                {
                    IdUserRole = (short)UserRoleEnum.PROFESSIONAL,
                    Code = "PROFESSIONAL",
                    Name = "Profesional",
                    Description = "Acceso a las funcionalidades clínicas y gestión de la atención de pacientes.",
                    CreatedAt = SeedConstants.SeedDate
                },
                new UserRole
                {
                    IdUserRole = (short)UserRoleEnum.ASSISTANT,
                    Code = "ASSISTANT",
                    Name = "Asistente",
                    Description = "Acceso a las funcionalidades administrativas y operativas asignadas.",
                    CreatedAt = SeedConstants.SeedDate
                }
);
        }
    }
}