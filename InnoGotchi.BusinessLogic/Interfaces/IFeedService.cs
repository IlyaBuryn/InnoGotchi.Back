using InnoGotchi.BusinessLogic.Services;
using InnoGotchi.Components.DtoModels;

namespace InnoGotchi.BusinessLogic.Interfaces
{
    public interface IFeedService
    {
        Task<int?> FeedPetAsync(FeedDto feedData, FeedActionType feedActionType);
        Task<double?> GetFeedPeriodsAsync(int farmId, FeedActionType feedActionType);
        Task RecalculateVitalSignsAsync(int farmId);
    }
}
