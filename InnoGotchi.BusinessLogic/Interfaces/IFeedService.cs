using InnoGotchi.BusinessLogic.Services;
using InnoGotchi.Components.DtoModels;

namespace InnoGotchi.BusinessLogic.Interfaces
{
    public interface IFeedService
    {
        public Task<int?> FeedPet(FeedDto feedData, FeedActionType feedActionType);
        public double? GetFeedPeriods(int farmId, FeedActionType feedActionType);
        public Task RecalculatePetsNeeds(int farmId);
    }
}
