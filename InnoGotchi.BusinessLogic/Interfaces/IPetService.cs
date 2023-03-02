using InnoGotchi.Components.DtoModels;
using InnoGotchi.Components.Enums;

namespace InnoGotchi.BusinessLogic.Interfaces
{
    public interface IPetService
    {
        Task<int?> AddNewPetAsync(PetDto petToAdd);
        Task<bool> UpdatePetAsync(PetDto petToUpdate);
        Task<bool> RemovePetAsync(int petId);
        Task<List<PetDto>> GetPetsAsPageAsync(int pageNumber, int pageSize, SortFilter sortFilter);
        Task<int> GetAllPetsCountAsync();
        Task<List<PetDto>> GetPetsByFarmIdAsync(int farmId);
        Task<PetDto?> GetPetByIdAsync(int petId);
    }
}
