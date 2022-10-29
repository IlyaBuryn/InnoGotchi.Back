using InnoGotchi.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InnoGotchi.DataAccess.Data.Configuration
{
    public class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.HasIndex(e => e.Name).IsUnique();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(64);
            builder.Property(e => e.FarmId).IsRequired();
            builder.Property(e => e.CreationDate).IsRequired().HasDefaultValueSql("GetDate()");

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
