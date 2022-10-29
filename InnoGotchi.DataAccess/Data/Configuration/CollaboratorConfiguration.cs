using InnoGotchi.DataAccess.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.DataAccess.Data.Configuration
{
    public class CollaboratorConfiguration : IEntityTypeConfiguration<Collaborator>
    {
        public void Configure(EntityTypeBuilder<Collaborator> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.FarmId).IsRequired();
            builder.Property(c => c.IdentityUserId).IsRequired();
        }

    }
}
