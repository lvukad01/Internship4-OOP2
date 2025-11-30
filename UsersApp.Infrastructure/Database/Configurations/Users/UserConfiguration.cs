using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using UsersApp.Domain.Entities.Users;

namespace UsersApp.Infrastructure.Database.Configurations.Users
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        { 
            builder.ToTable("users");
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(User.NameMaxLength);

            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(100);
            builder.HasIndex(u => u.Username).IsUnique();

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);
            builder.HasIndex(u => u.Email).IsUnique();

            builder.Property(u => u.AddressStreet)
                .IsRequired()
                .HasMaxLength(User.AddressStreetMaxLength);

            builder.Property(u => u.AddressCity)
                .IsRequired()
                .HasMaxLength(User.AddressCityMaxLength);

            builder.Property(u => u.GeoLat)
                .IsRequired();

            builder.Property(u => u.GeoLng)
                .IsRequired();

            builder.Property(u => u.Website)
                .HasMaxLength(User.WebsiteMaxLength);

            builder.Property(u => u.Password)
                .IsRequired();

            builder.Property(u => u.IsActive)
                .HasDefaultValue(true);

            builder.Property(u => u.CreatedAt)
                .IsRequired();

            builder.Property(u => u.UpdatedAt)
                .IsRequired();
        }
    }
}
