namespace InnoGotchi.Components.DtoModels
{
    public class VitalSignDto : DtoBase
    {
        public int PetId { get; set; }
        public int HungerLevel { get; set; }
        public int ThirstyLevel { get; set; }
        public int HappinessDaysCount { get; set; }
        public bool IsAlive { get; set; }

        public virtual PetDto? Pet { get; set; }
    }
}
