using InnoGotchi.Components.DtoModels;

namespace InnoGotchi.BusinessLogic.Interfaces
{
    public interface IBodyPartService
    {
        public Task<int?> AddNewBodyPartAsync(BodyPartDto bodyPartToAdd);
        public Task<bool> UpdateBodyPartAsync(BodyPartDto bodyPartToUpdate);
        public Task<bool> RemoveBodyPartAsync(int bodyPartId);
        public List<BodyPartDto> GetBodyParts();
        public List<BodyPartDto> GetBodyPartsByPetId(int petId);
        public List<BodyPartDto> GetBodyPartsByTypeId(int typeId);
        public Task<BodyPartDto?> GetBodyPartByIdAsync(int bodyPartId);


        public Task<int?> CreateBodyPartTypeAsync(BodyPartTypeDto bodyPartType);
        public Task<bool> DeleteBodyPartTypeAsync(int bodyPartTypeId);
    }
}
