using InnoGotchi.DataAccess.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.DataAccess.Data.Configuration
{
    public class BodyPartTypeConfiguration : IEntityTypeConfiguration<BodyPartType>
    {
        public void Configure(EntityTypeBuilder<BodyPartType> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();
            builder.Property(b => b.Name).IsRequired();

            builder.HasData(
                new BodyPartType()
                {
                    Id = 1,
                    Name = "Body",
                },
                new BodyPartType()
                {
                    Id = 2,
                    Name = "Nose",
                },
                new BodyPartType()
                {
                    Id = 3,
                    Name = "Mouth",
                },
                new BodyPartType()
                {
                    Id = 4,
                    Name = "Eye"
                }
            );
        }
    }
}
