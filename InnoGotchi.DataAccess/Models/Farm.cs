namespace InnoGotchi.DataAccess.Models
{
    public class Farm : EntityBase
    {
        public string Name { get; set; }
        public int IdentityUserId { get; set; }

        public virtual IdentityUser IdentityUser { get; set; }
        public virtual ICollection<Pet> Pets { get; set; } = null!;
    }
}
