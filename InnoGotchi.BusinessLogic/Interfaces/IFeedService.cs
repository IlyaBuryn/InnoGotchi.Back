using InnoGotchi.BusinessLogic.Dto;

namespace InnoGotchi.BusinessLogic.Interfaces
{
    public interface IFeedService
    {
        public Task<int?> FeedPet(FeedDto feedData);
        public Task RecalculatePetsNeeds(int farmId);
    }
}
