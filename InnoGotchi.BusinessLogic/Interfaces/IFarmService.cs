using InnoGotchi.Components.DtoModels;

namespace InnoGotchi.BusinessLogic.Interfaces
{
    public interface IFarmService
    {
        Task<int?> CreateFarmAsync(FarmDto farmToCreate);
        Task<bool> DeleteFarmAsync(int farmId);
        Task<bool> UpdateFarmAsync(FarmDto farmToUpdate);
        Task<FarmDto?> GetFarmByIdAsync(int farmId);
        Task<FarmDto?> GetFarmByUserIdAsync(int userId);
    }
}
