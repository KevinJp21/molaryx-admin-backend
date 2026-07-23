using Domain.Entities;
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

            var currentDate = new DateTime(
                2026, 7, 22,
                0, 0, 0,
                DateTimeKind.Utc
            );
            
            builder.HasData(
                new UserStatus
                {
                    IdUserStatus = 1,
                    Name = "Activo",
                    CreatedAt = currentDate,
                    UpdatedAt = null
                },
                new UserStatus
                {
                    IdUserStatus = 2,
                    Name = "Inactivo",
                    CreatedAt = currentDate,
                    UpdatedAt = null
                },
                new UserStatus
                {
                    IdUserStatus = 3,
                    Name = "Habilitación pendiente",
                    CreatedAt = currentDate,
                    UpdatedAt = null
                },
                new UserStatus
                {
                    IdUserStatus = 4,
                    Name = "Bloqueado",
                    CreatedAt = currentDate,
                    UpdatedAt = null
                }
            );
        }
    }
}