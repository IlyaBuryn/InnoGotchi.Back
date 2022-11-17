namespace InnoGotchi.BusinessLogic.Dto
{
    public class FeedDto : DtoBase
    {
        public int PetId { get; set; }
        public int IdentityUserId { get; set; }
        public int FoodCount { get; set; }
        public int WaterCount { get; set; }
        public DateTime FeedTime { get; set; }
    }
}
