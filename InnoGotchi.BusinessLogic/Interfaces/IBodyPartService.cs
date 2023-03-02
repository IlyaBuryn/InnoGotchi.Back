using InnoGotchi.Components.DtoModels;

namespace InnoGotchi.BusinessLogic.Interfaces
{
    public interface IBodyPartService
    {
        Task<int?> AddNewBodyPartAsync(BodyPartDto bodyPartToAdd);
        Task<bool> UpdateBodyPartAsync(BodyPartDto bodyPartToUpdate);
        Task<bool> RemoveBodyPartAsync(int bodyPartId);
        Task<List<BodyPartDto>> GetBodyPartsAsync();
        Task<List<BodyPartDto>> GetBodyPartsByPetIdAsync(int petId);
        Task<List<BodyPartDto>> GetBodyPartsByTypeIdAsync(int bodyPartTypeId);
        Task<BodyPartDto?> GetBodyPartByIdAsync(int bodyPartId);
        Task<int?> CreateBodyPartTypeAsync(BodyPartTypeDto bodyPartTypeToAdd);
        Task<bool> DeleteBodyPartTypeAsync(int bodyPartTypeId);
    }
}
