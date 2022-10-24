namespace InnoGotchi.DataAccess.Models
{
    public class Pet : EntityBase
    {
        public string Name { get; set; }
        public Farm Farm { get; set; }
        public BodyPart? Body { get; set; }
        public BodyPart? Eye { get; set; }
        public BodyPart? Nose { get; set; }
        public BodyPart? Mouth { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
