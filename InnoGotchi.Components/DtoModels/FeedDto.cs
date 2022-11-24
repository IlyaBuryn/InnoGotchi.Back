namespace InnoGotchi.Components.DtoModels
{
    public class FeedDto : DtoBase
    {
        public int PetId { get; set; }
        public int IdentityUserId { get; set; }
        public int FoodCount { get; set; }
        public int WaterCount { get; set; }
        public DateTime FeedTime { get; set; }

        public FeedDto() { }

        public FeedDto(int petId, int userId)
        {
            IdentityUserId = userId;
            FeedTime = DateTime.Now;
            FoodCount = 1;
            WaterCount = 1;
            PetId = petId;
        }
    }
}
