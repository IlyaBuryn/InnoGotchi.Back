using InnoGotchi.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InnoGotchi.DataAccess.Data.Configuration
{
    public class BodyPartPetConfiguration : IEntityTypeConfiguration<BodyPartPet>
    {
        public void Configure(EntityTypeBuilder<BodyPartPet> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();

            builder.HasData(
                new BodyPartPet()
                {
                    Id = 1,
                    BodyPartsId = 1,
                    PetsId = 1
                },
                new BodyPartPet()
                {
                    Id = 2,
                    BodyPartsId = 2,
                    PetsId = 1
                },
                new BodyPartPet()
                {
                    Id = 3,
                    BodyPartsId = 3,
                    PetsId = 1
                },
                new BodyPartPet()
                {
                    Id = 4,
                    BodyPartsId = 4,
                    PetsId = 1
                },


                new BodyPartPet()
                {
                    Id = 5,
                    BodyPartsId = 5,
                    PetsId = 2
                },
                new BodyPartPet()
                {
                    Id = 6,
                    BodyPartsId = 6,
                    PetsId = 2
                },
                new BodyPartPet()
                {
                    Id = 7,
                    BodyPartsId = 7,
                    PetsId = 2
                },
                new BodyPartPet()
                {
                    Id = 8,
                    BodyPartsId = 8,
                    PetsId = 2
                },


                new BodyPartPet()
                {
                    Id = 9,
                    BodyPartsId = 9,
                    PetsId = 3
                },
                new BodyPartPet()
                {
                    Id = 10,
                    BodyPartsId = 10,
                    PetsId = 3
                },
                new BodyPartPet()
                {
                    Id = 11,
                    BodyPartsId = 11,
                    PetsId = 3
                },
                new BodyPartPet()
                {
                    Id = 12,
                    BodyPartsId = 12,
                    PetsId = 3
                });

        }
    }
}
