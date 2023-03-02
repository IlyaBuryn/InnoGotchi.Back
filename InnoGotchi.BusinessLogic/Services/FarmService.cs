using AutoMapper;
using InnoGotchi.BusinessLogic.Exceptions;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.Components.DtoModels;
using InnoGotchi.DataAccess.Interfaces;
using InnoGotchi.DataAccess.Models;

namespace InnoGotchi.BusinessLogic.Services
{
    public class FarmService : IFarmService
    {
        private readonly IRepository<Farm> _farmRep;
        private readonly IMapper _mapper;

        public FarmService(IRepository<Farm> farmRep,
            IMapper mapper)
        { 
            _farmRep = farmRep;
            _mapper = mapper;
        }

        public async Task<int?> CreateFarmAsync(FarmDto farmToCreate)
        {
            var farmName = await _farmRep.GetOneAsync(x => x.Name == farmToCreate.Name);
            if (farmName != null)
            {
                throw new DataValidationException("This farm is already exist!");
            }

            var farmWithUser = await _farmRep
                .GetOneAsync(x => x.IdentityUserId == farmToCreate.IdentityUserId);

            if (farmWithUser != null)
            {
                throw new NotFoundException(nameof(farmWithUser));
            }

            var farm = _mapper.Map<Farm>(farmToCreate);
            return await _farmRep.AddAsync(farm);
        }

        public async Task<bool> DeleteFarmAsync(int farmId)
        {
            var farm = await _farmRep.GetOneByIdAsync(farmId);
            if (farm == null)
            {
                throw new NotFoundException(nameof(farm));
            }

            return await _farmRep.RemoveAsync(farmId);
        }

        public async Task<FarmDto?> GetFarmByIdAsync(int farmId)
        {
            var farm = await _farmRep.GetOneByIdAsync(farmId);
            if (farm == null)
            {
                throw new NotFoundException(nameof(farm));
            }

            return _mapper.Map<FarmDto>(farm);
        }

        public async Task<FarmDto?> GetFarmByUserIdAsync(int userId)
        {
            var farm = await _farmRep.GetOneAsync(x => x.IdentityUserId == userId);
            if (farm == null)
            {
                throw new NotFoundException(nameof(farm));
            }

            return _mapper.Map<FarmDto>(farm);
        }

        public async Task<bool> UpdateFarmAsync(FarmDto farmToUpdate)
        {
            var farm = await _farmRep.GetOneByIdAsync(farmToUpdate.Id);
            if (farm == null)
            {
                throw new NotFoundException(nameof(farm));
            }

            var farmName = await _farmRep.GetOneAsync(x => x.Name == farmToUpdate.Name);
            if (farmName != null)
            {
                throw new DataValidationException("This farm is already exist!");
            }

            farm.Name = farmToUpdate.Name;

            return await _farmRep.UpdateAsync(farm);
        }
    }
}
