using InnoGotchi.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InnoGotchi.DataAccess.Data.Configuration
{
    public class IdentityUserConfiguration : IEntityTypeConfiguration<IdentityUser>
    {
        public void Configure(EntityTypeBuilder<IdentityUser> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd();
            builder.HasIndex(u => u.Username).IsUnique();
            builder.Property(u => u.Username).IsRequired().HasMaxLength(128);
            builder.Property(u => u.Password).IsRequired().HasMaxLength(128);
            builder.Property(u => u.IdentityRoleId).IsRequired();

            builder.HasData(
                new IdentityUser()
                {
                    Id = 1,
                    Username = "admin",
                    Password = "admin",
                    Name = "Ilya",
                    IdentityRoleId = 1,
                },
                new IdentityUser()
                {
                    Id = 2,
                    Username = "ConfUser1@m.com",
                    Password = "123456",
                    Name = "ConfName1",
                    Surname = "ConfSurname1",
                    IdentityRoleId = 2,
                },
                new IdentityUser()
                {
                    Id = 3,
                    Username = "ConfUser2@m.com",
                    Password = "234567",
                    Name = "ConfName2",
                    Surname = "ConfSurname2",
                    IdentityRoleId = 2,
                }
            );
        }
    }
}
