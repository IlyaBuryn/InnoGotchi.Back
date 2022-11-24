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
                    Image = "~/images/bodies/body_1.svg",
                    Color = null,
                    BodyPartTypeId = 1,
                },
                new BodyPart()
                {
                    Id = 2,
                    Image = "~/images/noses/nose_1.svg",
                    Color = null,
                    BodyPartTypeId = 2,
                },
                new BodyPart()
                {
                    Id = 3,
                    Image = "~/images/mouths/mouth_1.svg",
                    Color = null,
                    BodyPartTypeId = 3,
                },
                new BodyPart()
                {
                    Id = 4,
                    Image = "~/images/eyes/eye_1.svg",
                    Color = null,
                    BodyPartTypeId = 4,
                },
                new BodyPart()
                {
                    Id = 5,
                    Image = "~/images/bodies/body_2.svg",
                    Color = null,
                    BodyPartTypeId = 1,
                },
                new BodyPart()
                {
                    Id = 6,
                    Image = "~/images/noses/nose_2.svg",
                    Color = null,
                    BodyPartTypeId = 2,
                },
                new BodyPart()
                {
                    Id = 7,
                    Image = "~/images/mouths/mouth_2.svg",
                    Color = null,
                    BodyPartTypeId = 3,
                },
                new BodyPart()
                {
                    Id = 8,
                    Image = "~/images/eyes/eye_2.svg",
                    Color = null,
                    BodyPartTypeId = 4,
                },
                new BodyPart()
                {
                    Id = 9,
                    Image = "~/images/bodies/body_3.svg",
                    Color = null,
                    BodyPartTypeId = 1,
                },
                new BodyPart()
                {
                    Id = 10,
                    Image = "~/images/noses/nose_3.svg",
                    Color = null,
                    BodyPartTypeId = 2,
                },
                new BodyPart()
                {
                    Id = 11,
                    Image = "~/images/mouths/mouth_3.svg",
                    Color = null,
                    BodyPartTypeId = 3,
                },
                new BodyPart()
                {
                    Id = 12,
                    Image = "~/images/eyes/eye_3.svg",
                    Color = null,
                    BodyPartTypeId = 4,
                },
                new BodyPart()
                {
                    Id = 13,
                    Image = "~/images/bodies/body_4.svg",
                    Color = null,
                    BodyPartTypeId = 1,
                },
                new BodyPart()
                {
                    Id = 14,
                    Image = "~/images/noses/nose_4.svg",
                    Color = null,
                    BodyPartTypeId = 2,
                },
                new BodyPart()
                {
                    Id = 15,
                    Image = "~/images/mouths/mouth_4.svg",
                    Color = null,
                    BodyPartTypeId = 3,
                },
                new BodyPart()
                {
                    Id = 16,
                    Image = "~/images/eyes/eye_4.svg",
                    Color = null,
                    BodyPartTypeId = 4,
                },
                new BodyPart()
                {
                    Id = 17,
                    Image = "~/images/bodies/body_5.svg",
                    Color = null,
                    BodyPartTypeId = 1,
                },
                new BodyPart()
                {
                    Id = 18,
                    Image = "~/images/noses/nose_5.svg",
                    Color = null,
                    BodyPartTypeId = 2,
                },
                new BodyPart()
                {
                    Id = 19,
                    Image = "~/images/mouths/mouth_5.svg",
                    Color = null,
                    BodyPartTypeId = 3,
                },
                new BodyPart()
                {
                    Id = 20,
                    Image = "~/images/eyes/eye_5.svg",
                    Color = null,
                    BodyPartTypeId = 4,
                }, 
                new BodyPart()
                {
                    Id = 21,
                    Image = "~/images/noses/nose_6.svg",
                    Color = null,
                    BodyPartTypeId = 2,
                },
                new BodyPart()
                {
                    Id = 22,
                    Image = "~/images/eyes/eye_6.svg",
                    Color = null,
                    BodyPartTypeId = 4,
                }
            );
        }
    }
}
