using InnoGotchi.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InnoGotchi.DataAccess.Data.Configuration
{
    public class FarmConfiguration : IEntityTypeConfiguration<Farm>
    {
        public void Configure(EntityTypeBuilder<Farm> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).ValueGeneratedOnAdd();
            builder.HasIndex(f => f.Name).IsUnique();
            builder.Property(f => f.Name).HasMaxLength(128);
            builder.HasIndex(f => f.IdentityUserId).IsUnique();
            builder.Property(f => f.IdentityUserId).IsRequired();
            builder.HasOne(f => f.IdentityUser).WithMany().OnDelete(DeleteBehavior.NoAction);

            builder.HasData
            (
                new Farm()
                {
                    Id = 1,
                    Name = "ConfTestFarm_1",
                    IdentityUserId = 1,
                },
                new Farm()
                {
                    Id = 2,
                    Name = "ConfTestFarm_2",
                    IdentityUserId = 2,
                }
            );
        }
    }
}
