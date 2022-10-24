namespace InnoGotchi.BusinessLogic.Dto
{
    public class VitalSignDto : DtoBase
    {
        public int PetId { get; set; }
        public int HungerLevel { get; set; }
        public int ThirsyLevel { get; set; }
        public int HappinessDaysCount { get; set; }
        public bool IsAlive { get; set; }
    }
}
