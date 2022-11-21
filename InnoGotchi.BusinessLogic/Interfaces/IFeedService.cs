using InnoGotchi.BusinessLogic.Dto;
using InnoGotchi.BusinessLogic.Services;

namespace InnoGotchi.BusinessLogic.Interfaces
{
    public interface IFeedService
    {
        public Task<int?> FeedPet(FeedDto feedData, FeedActionType feedActionType);
        public Task<double?> GetFeedPeriods(int farmId, FeedActionType feedActionType);
        public Task RecalculatePetsNeeds(int farmId);
    }
}
