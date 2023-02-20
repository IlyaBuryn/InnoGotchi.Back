using InnoGotchi.Components.DtoModels;
using InnoGotchi.Components.Enums;

namespace InnoGotchi.BusinessLogic.Interfaces
{
    public interface IPetService
    {
        public Task<int?> AddNewPetAsync(PetDto petToAdd);
        public Task<bool> UpdatePetAsync(PetDto petToUpdate);
        public Task<bool> RemovePetAsync(int petId);
        public Task<List<PetDto>> GetPetsAsyncAsPageAsync(int pageNumber, int pageSize, SortFilter sortFilter);
        public int GetAllPetsCount();
        public List<PetDto> GetPetsByFarmId(int farmId);
        public PetDto? GetPetById(int petId);
    }
}
