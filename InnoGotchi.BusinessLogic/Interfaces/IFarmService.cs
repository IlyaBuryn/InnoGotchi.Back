using InnoGotchi.BusinessLogic.Dto;

namespace InnoGotchi.BusinessLogic.Interfaces
{
    public interface IFarmService
    {
        public Task<int?> CreateNewFarmAsync(FarmDto farm);
        public Task<bool> DeleteFarmAsync(int farmId);
        public Task<FarmDto?> GetFarmByIdAsync(int farmId);
    }
}
