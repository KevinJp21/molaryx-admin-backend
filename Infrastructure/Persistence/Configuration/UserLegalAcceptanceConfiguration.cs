using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class UserLegalAcceptanceConfiguration : IEntityTypeConfiguration<UserLegalAcceptance>
    {
        public void Configure(EntityTypeBuilder<UserLegalAcceptance> builder)
        {
            builder.ToTable("user_legal_acceptances");

            builder.HasKey(a => a.IdUserLegalAcceptance);

            builder.Property(a => a.IdUser).IsRequired();
            builder.Property(a => a.IdDocumentType).IsRequired();

            builder.Property(a => a.DocumentVersion)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(a => a.AcceptedAt).IsRequired();

            builder.Property(a => a.IpAddress)
                .HasMaxLength(45);

            builder.Property(a => a.UserAgent)
                .HasMaxLength(512);

            builder.HasIndex(a => new { a.IdUser, a.IdDocumentType, a.DocumentVersion });

            builder.HasOne(a => a.User)
                .WithMany(u => u.LegalAcceptances)
                .HasForeignKey(a => a.IdUser)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
