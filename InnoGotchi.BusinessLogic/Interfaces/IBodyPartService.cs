using InnoGotchi.BusinessLogic.Dto;

namespace InnoGotchi.BusinessLogic.Interfaces
{
    public interface IBodyPartService
    {
        public Task<int?> AddNewBodyPartAsync(BodyPartDto bodyPartToAdd);
        public Task<bool> UpdateBodyPartAsync(BodyPartDto bodyPartToUpdate);
        public Task<bool> RemoveBodyPartAsync(int bodyPartId);
        public Task<List<BodyPartDto>> GetBodyPartsAsync();
        public Task<List<BodyPartDto>> GetBodyPartsByPetIdAsync(int petId);
        public Task<List<BodyPartDto>> GetBodyPartsByTypeIdAsync(int typeId);
        public Task<BodyPartDto?> GetBodyPartByIdAsync(int bodyPartId);


        public Task<int?> CreateBodyPartTypeAsync(BodyPartTypeDto bodyPartType);
        public Task<bool> DeleteBodyPartTypeAsync(int bodyPartTypeId);
    }
}
