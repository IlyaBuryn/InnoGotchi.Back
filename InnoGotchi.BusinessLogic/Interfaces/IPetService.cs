using InnoGotchi.BusinessLogic.Components;
using InnoGotchi.BusinessLogic.Dto;

namespace InnoGotchi.BusinessLogic.Interfaces
{
    public interface IPetService
    {
        public Task<Page<PetDto>> GetPetsAsync(int pageNumber, int pageSize);
        public Task<IQueryable<PetDto>> GetPetsByFarmIdAsync(int farmId);
        public Task<PetDto?> GetPetByIdAsync(int id);
        public Task<int?> AddNewPetAsync(PetDto pet);
        public Task<bool> RemovePetAsync(int petId);
        public Task UpdatePetAsync(PetDto pet);
    }
}
