using InnoGotchi.BusinessLogic.Services;
using InnoGotchi.Components.DtoModels;

namespace InnoGotchi.BusinessLogic.Interfaces
{
    public interface IFeedService
    {
        Task<int?> FeedPetAsync(FeedDto feedData, FeedActionType feedActionType);
        double? GetFeedPeriods(int farmId, FeedActionType feedActionType);
        Task RecalculatePetsNeedsAsync(int farmId);
    }
}
