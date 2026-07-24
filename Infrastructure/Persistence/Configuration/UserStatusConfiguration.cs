using Domain.Entities;
using Domain.Enums;
using Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class UserStatusConfiguration : IEntityTypeConfiguration<UserStatus>
    {
        public void Configure(EntityTypeBuilder<UserStatus> builder)
        {
            builder.ToTable("user_statuses");

            builder.HasKey(s => s.IdUserStatus);

            builder.Property(s => s.Name).IsRequired().HasMaxLength(255);

            builder.Property(s => s.CreatedAt).IsRequired();

            builder.Property(s => s.UpdatedAt);
            
            builder.HasData(
                new UserStatus
                {
                    IdUserStatus = (short)UserStatusEnum.ACTIVO,
                    Name = "Activo",
                    CreatedAt = SeedConstants.SeedDate,
                    UpdatedAt = null
                },
                new UserStatus
                {
                    IdUserStatus = (short)UserStatusEnum.INACTIVO,
                    Name = "Inactivo",
                    CreatedAt = SeedConstants.SeedDate,
                    UpdatedAt = null
                },
                new UserStatus
                {
                    IdUserStatus = (short)UserStatusEnum.HABILITACION_PENDIENTE,
                    Name = "Habilitación pendiente",
                    CreatedAt = SeedConstants.SeedDate,
                    UpdatedAt = null
                },
                new UserStatus
                {
                    IdUserStatus = (short)UserStatusEnum.BLOQUEADO,
                    Name = "Bloqueado",
                    CreatedAt = SeedConstants.SeedDate,
                    UpdatedAt = null
                }
            );
        }
    }
}