using InnoGotchi.Components.DtoModels;

namespace InnoGotchi.BusinessLogic.Interfaces
{
    public interface ICollaboratorService
    {
        Task<int?> CreateCollaboratorAsync(CollaboratorDto collaboratorToCreate);
        Task<bool> DeleteCollaboratorByUserIdAsync(int collaboratorId);
        Task<List<CollaboratorDto>> GetAllCollaboratorsByFarmAsync(int farmId);
        Task<List<CollaboratorDto>> GetAllCollaboratorsByUserAsync(int userId);
    }
}
