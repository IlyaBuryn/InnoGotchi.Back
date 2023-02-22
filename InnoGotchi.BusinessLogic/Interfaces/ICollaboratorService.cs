using InnoGotchi.Components.DtoModels;

namespace InnoGotchi.BusinessLogic.Interfaces
{
    public interface ICollaboratorService
    {
        Task<int?> CreateCollaboratorAsync(CollaboratorDto collaboratorToCreate);
        Task<bool> DeleteCollaboratorByUserIdAsync(int collaboratorId);
        List<CollaboratorDto> GetAllCollaboratorsByFarm(int farmId);
        List<CollaboratorDto> GetAllCollaboratorsByUser(int userId);
    }
}
