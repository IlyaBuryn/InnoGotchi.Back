using InnoGotchi.Components.DtoModels;
using InnoGotchi.Components.Enums;

namespace InnoGotchi.BusinessLogic.Interfaces
{
    public interface IPetService
    {
        public Task<int?> AddNewPetAsync(PetDto petToAdd);
        public Task<bool> UpdatePetAsync(PetDto petToUpdate);
        public Task<bool> RemovePetAsync(int petId);
        public Task<List<PetDto>> GetPetsAsyncAsPage(int pageNumber, int pageSize, SortFilter sortFilter);
        public Task<int> GetAllPetsCount();
        public Task<List<PetDto>> GetPetsByFarmIdAsync(int farmId);
        public Task<PetDto?> GetPetByIdAsync(int petId);
    }
}
