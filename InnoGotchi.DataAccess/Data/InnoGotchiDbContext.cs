using InnoGotchi.DataAccess.Data.Configuration;
using InnoGotchi.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.DataAccess.Data
{
    public class InnoGotchiDbContext : DbContext
    {
        public InnoGotchiDbContext(DbContextOptions<InnoGotchiDbContext> options)
           : base(options)
        {
        }

        public DbSet<IdentityRole> IdentityRoles { get; set; }
        public DbSet<IdentityUser> IdentityUsers { get; set; }
        public DbSet<Collaborator> Collaborators { get; set; }
        public DbSet<Farm> Farms { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<VitalSign> VitalSigns { get; set; }
        public DbSet<BodyPartType> BodyPartTypes { get; set; }
        public DbSet<BodyPart> BodyParts { get; set; }
        public DbSet<BodyPartPet> BodyPartPets { get; set; }
        public DbSet<Feed> FeedsInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new IdentityRoleConfiguration());
            modelBuilder.ApplyConfiguration(new IdentityUserConfiguration());
            modelBuilder.ApplyConfiguration(new CollaboratorConfiguration());
            modelBuilder.ApplyConfiguration(new FarmConfiguration());
            modelBuilder.ApplyConfiguration(new PetConfiguration());
            modelBuilder.ApplyConfiguration(new VitalSignConfiguration());
            modelBuilder.ApplyConfiguration(new BodyPartTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BodyPartConfiguration());
            modelBuilder.ApplyConfiguration(new FeedConfiguration());
            modelBuilder.ApplyConfiguration(new BodyPartPetConfiguration());
        }
    }
}