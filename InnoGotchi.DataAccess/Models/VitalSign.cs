namespace InnoGotchi.DataAccess.Models
{
    public class VitalSign : EntityBase
    {
        public int PetId { get; set; }
        public int HungerLevel { get; set; }
        public int ThirstyLevel { get; set; }
        public int HappinessDaysCount { get; set; }
        public bool IsAlive { get; set; }

        public virtual Pet Pet { get; set; }
    }
}
