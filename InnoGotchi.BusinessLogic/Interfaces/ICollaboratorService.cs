using InnoGotchi.Components.DtoModels;

namespace InnoGotchi.BusinessLogic.Interfaces
{
    public interface ICollaboratorService
    {
        public Task<int?> CreateCollaboratorAsync(CollaboratorDto collaboratorToCreate);
        public Task<bool> DeleteCollaboratorByUserIdAsync(int collaboratorId);
        public Task<List<CollaboratorDto>> GetAllCollaboratorsByFarmAsync(int farmId);
        public Task<List<CollaboratorDto>> GetAllCollaboratorsByUserAsync(int userId);
    }
}
