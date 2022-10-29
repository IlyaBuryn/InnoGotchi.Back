using InnoGotchi.BusinessLogic.Dto;

namespace InnoGotchi.BusinessLogic.Interfaces
{
    public interface IBodyPartService
    {
        public Task<IQueryable<BodyPartDto>> GetBodyPartsAsync();
        public Task<IQueryable<BodyPartDto>> GetBodyPartsByTypeIdAsync(int typeId);
        public Task<BodyPartDto?> GetBodyPartByIdAsync(int bodyPartId);
        public Task<int?> AddNewBodyPartAsync(BodyPartDto bodyPart);
        public Task<int?> CreateBodyPartTypeAsync(BodyPartTypeDto bodyPartType);
        public Task<bool> DeleteBodyPartTypeAsync(int bodyPartTypeId);
        public Task<bool> RemoveBodyPartDtoAsync(int bodyPartId);
        public Task UpdateBodyPartAsync(BodyPartDto bodyPart);
    }
}
