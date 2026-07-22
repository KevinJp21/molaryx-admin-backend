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

            builder.Property(u => u.Name).IsRequired().HasMaxLength(255);

            builder.Property(u => u.Email).IsRequired();

            builder.Property(u => u.CreatedAt).IsRequired();

            builder.Property(u => u.UpdatedAt);

            builder.HasIndex(u => u.Email).IsUnique();
        }
    }
}