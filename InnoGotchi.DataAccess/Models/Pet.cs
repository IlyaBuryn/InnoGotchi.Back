namespace InnoGotchi.DataAccess.Models
{
    public class Pet : EntityBase
    {
        public string Name { get; set; }
        public int FarmId { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual Farm Farm { get; set; }
        public virtual ICollection<BodyPart> BodyParts { get; set; } = null!;
    }
}
