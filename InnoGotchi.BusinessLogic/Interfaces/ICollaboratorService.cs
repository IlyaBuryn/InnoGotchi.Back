using InnoGotchi.Components.DtoModels;

namespace InnoGotchi.BusinessLogic.Interfaces
{
    public interface ICollaboratorService
    {
        public Task<int?> CreateCollaboratorAsync(CollaboratorDto collaboratorToCreate);
        public Task<bool> DeleteCollaboratorByUserIdAsync(int collaboratorId);
        public List<CollaboratorDto> GetAllCollaboratorsByFarm(int farmId);
        public List<CollaboratorDto> GetAllCollaboratorsByUser(int userId);
    }
}
