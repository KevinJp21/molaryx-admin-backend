using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class UserSessionConfiguration : IEntityTypeConfiguration<UserSession>
    {
        public void Configure(EntityTypeBuilder<UserSession> builder)
        {
            builder.ToTable("user_sessions");

            builder.HasKey(s => s.IdUserSession);

            builder.Property(s => s.IdUser).IsRequired();

            builder.Property(s => s.RefreshTokenHash);

            builder.Property(s => s.ExpiresAt).IsRequired();

            builder.Property(s => s.RevokedAt);

            builder.Property(s => s.Device);

            builder.Property(s => s.IpConnection);

            builder.Property(u => u.LastLogin);

            builder.Property(s => s.CreatedAt).IsRequired();

            builder.Property(s => s.UpdatedAt);

            // Relaciones

            builder.HasOne(s => s.User)
                .WithMany(u => u.UserSessions)
                .HasForeignKey(s => s.IdUser)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.RefreshTokenHash) .IsUnique();
        }
    }
}