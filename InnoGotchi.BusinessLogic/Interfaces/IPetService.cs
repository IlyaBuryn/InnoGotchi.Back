using InnoGotchi.BusinessLogic.Dto;
using InnoGotchi.DataAccess.Components;

namespace InnoGotchi.BusinessLogic.Interfaces
{
    public interface IPetService
    {
        public Task<int?> AddNewPetAsync(PetDto petToAdd);
        public Task<bool> UpdatePetAsync(PetDto petToUpdate);
        public Task<bool> RemovePetAsync(int petId);
        public Task<Page<PetDto>> GetPetsAsync(int pageNumber, int pageSize);
        public Task<IQueryable<PetDto>> GetPetsByFarmIdAsync(int farmId);
        public Task<PetDto?> GetPetByIdAsync(int petId);
    }
}
