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
                    IdUserStatus = (short)UserStatusEnum.ACTIVE,
                    Name = "Activo",
                    CreatedAt = SeedConstants.SeedDate
                },
                new UserStatus
                {
                    IdUserStatus = (short)UserStatusEnum.INACTIVE,
                    Name = "Inactivo",
                    CreatedAt = SeedConstants.SeedDate
                },
                new UserStatus
                {
                    IdUserStatus = (short)UserStatusEnum.PENDING,
                    Name = "Pendiente",
                    CreatedAt = SeedConstants.SeedDate
                },
                new UserStatus
                {
                    IdUserStatus = (short)UserStatusEnum.BLOCKED,
                    Name = "Bloqueado",
                    CreatedAt = SeedConstants.SeedDate
                }
            );
        }
    }
}