using AutoMapper;
using FluentValidation;
using InnoGotchi.BusinessLogic.Dto;
using InnoGotchi.BusinessLogic.Exceptions;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.DataAccess.Interfaces;
using InnoGotchi.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.BusinessLogic.Services
{
    public class CollaboratorService : ICollaboratorService
    {
        private readonly IRepository<Collaborator> _collabRep;
        private readonly IRepository<Farm> _farmRep;
        private readonly IRepository<IdentityUser> _userRep;
        private readonly IValidator<Collaborator> _collabValidator;
        private readonly IMapper _mapper;

        public CollaboratorService(IRepository<Collaborator> collabRep,
            IRepository<Farm> farmRep,
            IRepository<IdentityUser> userRep,
            IValidator<Collaborator> collabValidator,
            IMapper mapper)
        {
            _collabRep = collabRep;
            _farmRep = farmRep;
            _userRep = userRep;
            _collabValidator = collabValidator;
            _mapper = mapper;
        }

        public async Task<int?> CreateCollaboratorAsync(CollaboratorDto collaboratorToCreate)
        {
            var validationResult = await _collabValidator.ValidateAsync(
                _mapper.Map<Collaborator>(collaboratorToCreate));

            if (!validationResult.IsValid)
                throw new DataValidationException();

            var existCollab = await _collabRep.GetOneAsync(x => x.IdentityUserId == collaboratorToCreate.IdentityUserId
                && x.FarmId == collaboratorToCreate.FarmId);
            if (existCollab != null)
                throw new DataValidationException("This user is already your friend!");

            var farm = await _farmRep.GetByIdAsync(collaboratorToCreate.FarmId);
            if (farm == null)
                throw new NotFoundException(nameof(farm));

            var user = await _userRep.GetByIdAsync(collaboratorToCreate.IdentityUserId);
            if (user == null)
                throw new NotFoundException(nameof(user));

            return await _collabRep.AddAsync(_mapper.Map<Collaborator>(collaboratorToCreate));
        }

        public async Task<bool> DeleteCollaboratorByUserIdAsync(int userId)
        {
            var collaborator = await _collabRep.GetOneAsync(x => x.IdentityUserId == userId);
            if (collaborator == null)
                throw new NotFoundException(nameof(collaborator));

            return await _collabRep.RemoveAsync(collaborator.Id);
        }

        public async Task<List<CollaboratorDto>> GetAllCollaboratorsByFarmAsync(int farmId)
        {
            var collabs = (await _collabRep.GetAllAsync(x => x.FarmId == farmId)).Include(x => x.Farm);
            if (collabs == null)
                throw new NotFoundException(nameof(collabs));

            return _mapper.Map<List<CollaboratorDto>>(collabs);
        }

        public async Task<List<CollaboratorDto>> GetAllCollaboratorsByUserAsync(int userId)
        {
            var collabs = (await _collabRep.GetAllAsync(x => x.IdentityUserId == userId)).Include(x => x.Farm);
            if (collabs == null)
                throw new NotFoundException(nameof(collabs));

            return _mapper.Map<List<CollaboratorDto>>(collabs);
        }
    }
}
