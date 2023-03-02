using InnoGotchi.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InnoGotchi.DataAccess.Data.Configuration
{
    public class FeedConfiguration : IEntityTypeConfiguration<Feed>
    {
        public void Configure(EntityTypeBuilder<Feed> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.FeedTime).IsRequired().HasDefaultValueSql("GetDate()");
            builder.Property(f => f.FoodCount).IsRequired().HasMaxLength(3);
            builder.Property(f => f.WaterCount).IsRequired().HasMaxLength(3);
            builder.Property(f => f.IdentityUserId).IsRequired(false);
            builder.HasOne(f => f.IdentityUser).WithMany().OnDelete(DeleteBehavior.NoAction);
            builder.Property(f => f.PetId).IsRequired();
            builder.HasOne(f => f.Pet).WithMany().OnDelete(DeleteBehavior.NoAction);
        }
    }
}
