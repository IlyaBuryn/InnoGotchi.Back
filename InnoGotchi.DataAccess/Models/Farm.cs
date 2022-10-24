namespace InnoGotchi.DataAccess.Models
{
    public class Farm : EntityBase
    {
        public string Name { get; set; }
        public virtual ICollection<Pet> Pets { get; set; }
    }
}
