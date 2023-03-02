using InnoGotchi.DataAccess.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.DataAccess.Data.Configuration
{
    public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedOnAdd();
            builder.Property(r => r.Name).IsRequired().HasMaxLength(64);

            builder.HasData(
                new IdentityRole()
                {
                    Id = 1,
                    Name = "Admin",
                },
                new IdentityRole()
                {
                    Id = 2,
                    Name = "User",
                }
            );

        }

    }
}
