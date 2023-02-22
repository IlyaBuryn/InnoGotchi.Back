using InnoGotchi.Components.DtoModels;

namespace InnoGotchi.BusinessLogic.Interfaces
{
    public interface IBodyPartService
    {
        Task<int?> AddNewBodyPartAsync(BodyPartDto bodyPartToAdd);
        Task<bool> UpdateBodyPartAsync(BodyPartDto bodyPartToUpdate);
        Task<bool> RemoveBodyPartAsync(int bodyPartId);
        List<BodyPartDto> GetBodyParts();
        List<BodyPartDto> GetBodyPartsByPetId(int petId);
        List<BodyPartDto> GetBodyPartsByTypeId(int typeId);
        Task<BodyPartDto?> GetBodyPartByIdAsync(int bodyPartId);


        Task<int?> CreateBodyPartTypeAsync(BodyPartTypeDto bodyPartType);
        Task<bool> DeleteBodyPartTypeAsync(int bodyPartTypeId);
    }
}
