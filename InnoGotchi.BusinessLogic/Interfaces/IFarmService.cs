using InnoGotchi.BusinessLogic.Dto;
using InnoGotchi.DataAccess.Components;

namespace InnoGotchi.BusinessLogic.Interfaces
{
    public interface IFarmService
    {
        public Task<int?> CreateFarmAsync(FarmDto farmToCreate);
        public Task<bool> DeleteFarmAsync(int farmId);
        public Task<bool> UpdateFarmAsync(FarmDto farmToUpdate);
        public Task<FarmDto?> GetFarmByIdAsync(int farmId);
        public Task<FarmDto?> GetFarmByUserIdAsync(int userId);
    }
}
