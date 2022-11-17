namespace InnoGotchi.BusinessLogic.Dto
{
    public class PetDto : DtoBase
    {
        public string Name { get; set; }
        public int FarmId { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public ICollection<BodyPartDto>? BodyParts { get; set; } = null!;
        public FarmDto? Farm { get; set; }
        public VitalSignDto? VitalSign { get; set; }
    }
}
