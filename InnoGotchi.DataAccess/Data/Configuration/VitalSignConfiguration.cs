using InnoGotchi.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InnoGotchi.DataAccess.Data.Configuration
{
    public class VitalSignConfiguration : IEntityTypeConfiguration<VitalSign>
    {
        public void Configure(EntityTypeBuilder<VitalSign> builder)
        {
            builder.HasKey(v => v.Id);
            builder.Property(v => v.Id).ValueGeneratedOnAdd();
            builder.HasIndex(v => v.PetId).IsUnique();
            builder.Property(v => v.PetId).IsRequired();
            builder.Property(v => v.HungerLevel).IsRequired().HasDefaultValue(0);
            builder.Property(v => v.ThirsyLevel).IsRequired().HasDefaultValue(0);
            builder.Property(v => v.HappinessDaysCount).IsRequired().HasDefaultValue(0);
            builder.Property(v => v.IsAlive).IsRequired().HasDefaultValue(true);

            builder.HasData(
                new VitalSign()
                {
                    Id = 1,
                    PetId = 1
                },
                new VitalSign()
                {
                    Id = 2,
                    PetId = 2,
                    HungerLevel = 1,
                    ThirsyLevel = 2,
                    HappinessDaysCount = 1,
                },
                new VitalSign()
                {
                    Id = 3,
                    PetId = 3,
                    HungerLevel = 2,
                    ThirsyLevel = 3,
                    HappinessDaysCount = 5,
                }
            );
        }
    }
}
