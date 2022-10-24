namespace InnoGotchi.DataAccess.Models
{
    public class BodyPart : EntityBase
    {
        public string Image { get; set; }
        public string Color { get; set; }
        public BodyPartType? BodyPartType { get; set; }
    }
}
