using InnoGotchi.Components.DtoModels;
using InnoGotchi.Components.Enums;

namespace InnoGotchi.BusinessLogic.Interfaces
{
    public interface IPetService
    {
        Task<int?> AddNewPetAsync(PetDto petToAdd);
        Task<bool> UpdatePetAsync(PetDto petToUpdate);
        Task<bool> RemovePetAsync(int petId);
        Task<List<PetDto>> GetPetsAsyncAsPageAsync(int pageNumber, int pageSize, SortFilter sortFilter);
        int GetAllPetsCount();
        List<PetDto> GetPetsByFarmId(int farmId);
        PetDto? GetPetById(int petId);
    }
}
