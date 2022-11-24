using System.ComponentModel.DataAnnotations.Schema;

namespace InnoGotchi.DataAccess.Models
{
    public class BodyPart : EntityBase
    {
        public string? Image { get; set; }
        public string? Color { get; set; }
        public int BodyPartTypeId { get; set; }

        public virtual BodyPartType BodyPartType { get; set; }
        public virtual ICollection<Pet> Pets { get; set; } = null!;
    }
}
