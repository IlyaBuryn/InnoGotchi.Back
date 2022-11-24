namespace InnoGotchi.DataAccess.Models
{
    public class BodyPartPet : EntityBase
    {
        public int BodyPartsId { get; set; }
        public int PetsId { get; set; }
        public BodyPart BodyPart { get; set; }
        public Pet Pet { get; set; }
    }
}
