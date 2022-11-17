using InnoGotchi.BusinessLogic.Dto;

namespace InnoGotchi.BusinessLogic.Interfaces
{
    public interface ICollaboratorService
    {
        public Task<int?> CreateCollaboratorAsync(CollaboratorDto collaboratorToCreate);
        public Task<bool> DeleteCollaboratorByUserIdAsync(int collaboratorId);
        public Task<List<CollaboratorDto>> GetAllCollaboratorsByFarmAsync(int farmId);
    }
}
