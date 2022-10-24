using AutoMapper;
using InnoGotchi.BusinessLogic.Dto;
using InnoGotchi.BusinessLogic.Exceptions;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.DataAccess.Interfaces;
using InnoGotchi.DataAccess.Models;

namespace InnoGotchi.BusinessLogic.Services
{
    public class FarmService : IFarmService
    {
        private readonly IRepository<Farm> _farmRep;
        private readonly IMapper _mapper;

        public FarmService(IRepository<Farm> farmRep, IMapper mapper)
        {
            _farmRep = farmRep;
            _mapper = mapper;
        }

        public async Task<int?> CreateNewFarmAsync(FarmDto farm)
        {
            return await _farmRep.AddAsync(_mapper.Map<Farm>(farm));
        }

        public async Task<bool> DeleteFarmAsync(int farmId)
        {
            if (await _farmRep.GetByIdAsync(farmId) == null)
                throw new NotFoundException("This farm does not exist!");

            return await _farmRep.RemoveAsync(farmId);
        }

        public async Task<FarmDto?> GetFarmByIdAsync(int farmId)
        {
            var farm = await _farmRep.GetByIdAsync(farmId);
            if (farm == null)
                throw new NotFoundException("This farm does not exist!");

            return _mapper.Map<FarmDto>(farm);
        }
    }
}
