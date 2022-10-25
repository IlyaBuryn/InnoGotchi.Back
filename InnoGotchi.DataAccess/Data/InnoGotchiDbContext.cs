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

        public DbSet<Pet> Pets { get; set; }
        public DbSet<Farm> Farms { get; set; }
        public DbSet<Collaborator> Collaborators { get; set; }
        public DbSet<VitalSign> VitalSigns { get; set; }
        public DbSet<BodyPart> BodyParts { get; set; }
        public DbSet<BodyPartType> BodyPartTypes { get; set; }
        public DbSet<IdentityUser> IdentityUsers { get; set; }
        public DbSet<IdentityRole> IdentityRoles { get; set; }
    }
}