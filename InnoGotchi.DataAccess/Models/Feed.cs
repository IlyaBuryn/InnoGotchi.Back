namespace InnoGotchi.DataAccess.Models
{
    public class Feed : EntityBase
    {
        public int PetId { get; set; }
        public int? IdentityUserId { get; set; }
        public DateTime FeedTime { get; set; }
        public int FoodCount { get; set; }
        public int WaterCount { get; set; }


        public virtual Pet Pet { get; set; }
        public virtual IdentityUser? IdentityUser { get; set; }
    }
}
