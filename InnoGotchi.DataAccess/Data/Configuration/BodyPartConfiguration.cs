using InnoGotchi.DataAccess.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.DataAccess.Data.Configuration
{
    public class BodyPartConfiguration : IEntityTypeConfiguration<BodyPart>
    {
        public void Configure(EntityTypeBuilder<BodyPart> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();
            builder.Property(b => b.BodyPartTypeId).IsRequired();

            builder.HasData(
                new BodyPart()
                {
                    Id = 1,
                    Image = "testbody_1.png",
                    Color = "#ffffff",
                    BodyPartTypeId = 1,
                },
                new BodyPart()
                {
                    Id = 2,
                    Image = "testnose_1.png",
                    Color = "#ffffff",
                    BodyPartTypeId = 2,
                },
                new BodyPart()
                {
                    Id = 3,
                    Image = "testmouth_1.png",
                    Color = "#ffffff",
                    BodyPartTypeId = 3,
                },
                new BodyPart()
                {
                    Id = 4,
                    Image = "testeye_1.png",
                    Color = "#ffffff",
                    BodyPartTypeId = 4,
                }
            );
        }
    }
}
