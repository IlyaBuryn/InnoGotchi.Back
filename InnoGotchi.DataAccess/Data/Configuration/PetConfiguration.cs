using InnoGotchi.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InnoGotchi.DataAccess.Data.Configuration
{
    public class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.HasIndex(p => p.Name).IsUnique();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(64);
            builder.Property(p => p.FarmId).IsRequired();
            builder.Property(p => p.CreationDate).IsRequired().HasDefaultValueSql("GetDate()");
            builder.HasMany(p => p.BodyParts).WithMany(bp => bp.Pets).UsingEntity<BodyPartPet>(
                x => x.HasOne(x => x.BodyPart)
                .WithMany().HasForeignKey(x => x.BodyPartsId),
                x => x.HasOne(x => x.Pet)
                .WithMany().HasForeignKey(x => x.PetsId));

            builder.HasData
            (
                new Pet
                {
                    Id = 1,
                    Name = "ConfTestName_1",
                    FarmId = 1
                },
                new Pet
                {
                    Id = 2,
                    Name = "ConfTestName_2",
                    FarmId = 1
                },
                new Pet
                {
                    Id = 3,
                    Name = "ConfTestName_3",
                    FarmId = 2
                }
            );
        }
    }
}
