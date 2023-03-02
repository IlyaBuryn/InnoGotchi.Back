using AutoMapper;
using InnoGotchi.BusinessLogic.Exceptions;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.Components.DtoModels;
using InnoGotchi.DataAccess.Interfaces;
using InnoGotchi.DataAccess.Models;

namespace InnoGotchi.BusinessLogic.Services
{
    public class CollaboratorService : ICollaboratorService
    {
        private readonly IRepository<Collaborator> _collaboratorRep;
        private readonly IRepository<Farm> _farmRep;
        private readonly IRepository<IdentityUser> _userRep;
        private readonly IMapper _mapper;

        public CollaboratorService(IRepository<Collaborator> collaboratorRep,
            IRepository<Farm> farmRep,
            IRepository<IdentityUser> userRep,
            IMapper mapper)
        {
            _collaboratorRep = collaboratorRep;
            _farmRep = farmRep;
            _userRep = userRep;
            _mapper = mapper;
        }

        public async Task<int?> CreateCollaboratorAsync(CollaboratorDto collaboratorToCreate)
        {
            var existCollaborator = await _collaboratorRep.GetOneAsync(
                x => x.IdentityUserId == collaboratorToCreate.IdentityUserId
                && x.FarmId == collaboratorToCreate.FarmId);

            if (existCollaborator != null)
            {
                throw new DataValidationException("This user is already your friend!");
            }

            var farm = await _farmRep.GetOneByIdAsync(collaboratorToCreate.FarmId);
            if (farm == null)
            {
                throw new NotFoundException(nameof(farm));
            }

            var user = await _userRep.GetOneByIdAsync(collaboratorToCreate.IdentityUserId);
            if (user == null)
            {
                throw new NotFoundException(nameof(user));
            }

            var collaborator = _mapper.Map<Collaborator>(collaboratorToCreate);
            return await _collaboratorRep.AddAsync(collaborator);
        }

        public async Task<bool> DeleteCollaboratorByUserIdAsync(int userId)
        {
            var collaborator = await _collaboratorRep.GetOneAsync(x => x.IdentityUserId == userId);
            if (collaborator == null)
            {
                throw new NotFoundException(nameof(collaborator));
            }

            return await _collaboratorRep.RemoveAsync(collaborator.Id);
        }

        public async Task<List<CollaboratorDto>> GetAllCollaboratorsByFarmAsync(int farmId)
        {
            var collaborators = await _collaboratorRep.GetManyAsync(
                expression: x => x.FarmId == farmId,
                includeProperties: x => x.Farm);

            if (collaborators == null)
            {
                throw new NotFoundException(nameof(collaborators));
            }

            return _mapper.Map<List<CollaboratorDto>>(collaborators);
        }

        public async Task<List<CollaboratorDto>> GetAllCollaboratorsByUserAsync(int userId)
        {
            var collaborators = await _collaboratorRep.GetManyAsync(
                expression: x => x.IdentityUserId == userId,
                includeProperties: x => x.Farm);

            if (collaborators == null)
            {
                throw new NotFoundException(nameof(collaborators));
            }

            return _mapper.Map<List<CollaboratorDto>>(collaborators);
        }
    }
}
