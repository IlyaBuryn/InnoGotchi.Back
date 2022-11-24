namespace InnoGotchi.Components.DtoModels
{
    public class PetDto : DtoBase
    {
        public string Name { get; set; }
        public int FarmId { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public List<BodyPartDto>? BodyParts { get; set; } = null!;
        public FarmDto? Farm { get; set; }
        public VitalSignDto? VitalSign { get; set; }
    }
}
