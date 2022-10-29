using AutoMapper;
using FluentValidation;
using InnoGotchi.BusinessLogic.Dto;
using InnoGotchi.BusinessLogic.Exceptions;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.DataAccess.Components;
using InnoGotchi.DataAccess.Interfaces;
using InnoGotchi.DataAccess.Models;

namespace InnoGotchi.BusinessLogic.Services
{
    public class FarmService : IFarmService
    {
        private readonly IRepository<Farm> _farmRep;
        private readonly IRepository<Collaborator> _collabRep;
        private readonly IValidator<Farm> _farmValidator;
        private readonly IMapper _mapper;

        public FarmService(IRepository<Farm> farmRep,
            IRepository<Collaborator> collabRep,
            IValidator<Farm> farmValidator,
            IMapper mapper)
        { 
            _farmValidator = farmValidator;
            _collabRep = collabRep;
            _farmRep = farmRep;
            _mapper = mapper;
        }

        public async Task<int?> CreateFarmAsync(FarmDto farmToCreate)
        {
            var validationResult = await _farmValidator.ValidateAsync(_mapper.Map<Farm>(farmToCreate));

            if (!validationResult.IsValid)
                throw new DataValidationException();

            var farm = await _farmRep.GetOneAsync(x => x.Name == farmToCreate.Name);
            if (farm != null)
                throw new DataValidationException("This farm is already exist!");

            var farmWithUser = await _farmRep
                .GetOneAsync(x => x.IdentityUserId == farmToCreate.IdentityUserId);
            if (farmWithUser != null)
                throw new NotFoundException(nameof(farmWithUser)); 

            return await _farmRep.AddAsync(_mapper.Map<Farm>(farmToCreate));
        }

        public async Task<bool> DeleteFarmAsync(int farmId)
        {
            var farm = await _farmRep.GetByIdAsync(farmId);
            if (farm == null)
                throw new NotFoundException(nameof(farm));

            return await _farmRep.RemoveAsync(farmId);
        }

        public async Task<FarmDto?> GetFarmByIdAsync(int farmId)
        {
            var farm = await _farmRep.GetByIdAsync(farmId);
            if (farm == null)
                throw new NotFoundException(nameof(farm));

            return _mapper.Map<FarmDto>(farm);
        }

        public async Task<FarmDto?> GetFarmByUserIdAsync(int userId)
        {
            var farm = await _farmRep.GetOneAsync(x => x.IdentityUserId == userId);
            if (farm == null)
                throw new NotFoundException(nameof(farm));

            return _mapper.Map<FarmDto>(farm);
        }

        public async Task<bool> UpdateFarmAsync(FarmDto farmToUpdate)
        {
            var validationResult = await _farmValidator.ValidateAsync(_mapper.Map<Farm>(farmToUpdate));

            if (!validationResult.IsValid)
                throw new DataValidationException();

            var farm = await _farmRep.GetByIdAsync(farmToUpdate.Id);
            if (farm == null)
                throw new NotFoundException(nameof(farm));

            var tmp = await _farmRep.GetOneAsync(x => x.Name == farmToUpdate.Name);
            if (tmp != null)
                throw new DataValidationException("Incorrect pet data!");

            farm.Name = farmToUpdate.Name;

            return await _farmRep.UpdateAsync(farm);
        }
    }
}
