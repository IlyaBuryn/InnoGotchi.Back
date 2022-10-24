namespace InnoGotchi.DataAccess.Models
{
    public class VitalSign : EntityBase
    {
        public Pet Pet { get; set; }
        public bool IsAlive { get; set; }
        public int HungerLevel { get; set; }
        public int ThirsyLevel { get; set; }
        public int HappinessDaysCount { get; set; }
    }
}
