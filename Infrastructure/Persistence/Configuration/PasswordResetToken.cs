using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class PasswordResetTokenConfiguration : IEntityTypeConfiguration<PasswordResetToken>
    {
        public void Configure(EntityTypeBuilder<PasswordResetToken> builder)
        {
            builder.ToTable("password_reset_tokens");

            builder.HasKey(p => p.IdPasswordResetToken);

            builder.Property(p => p.IdUser).IsRequired();

            builder.Property(p => p.Token).IsRequired().HasMaxLength(120);

            builder.Property(p => p.ExpiresAt).IsRequired();

            builder.Property(p => p.UsedAt);

            builder.Property(m => m.CreatedAt).IsRequired();

            builder.Property(m => m.UpdatedAt);

            builder.HasIndex(p => p.Token).IsUnique();

            builder.HasOne(p => p.User)
                .WithMany(u => u.PasswordResetTokens)
                .HasForeignKey(p => p.IdUser)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}