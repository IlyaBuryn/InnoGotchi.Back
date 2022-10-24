using AutoMapper;
using InnoGotchi.BusinessLogic.Components;
using InnoGotchi.BusinessLogic.Dto;
using InnoGotchi.BusinessLogic.Exceptions;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.DataAccess.Interfaces;
using InnoGotchi.DataAccess.Models;

namespace InnoGotchi.BusinessLogic.Services
{
    public class PetService : IPetService
    {
        private readonly IRepository<Pet> _petRep;
        private readonly IMapper _mapper;

        public PetService(IRepository<Pet> petRep, IMapper mapper)
        {
            _petRep = petRep;
            _mapper = mapper;
        }

        public async Task<int?> AddNewPetAsync(PetDto pet)
        {
            return await _petRep.AddAsync(_mapper.Map<Pet>(pet));
        }

        public async Task<PetDto?> GetPetByIdAsync(int id)
        {
            var pet = await _petRep.GetByIdAsync(id);
            if (pet == null)
                throw new NotFoundException("This pet does not exist!");

            return _mapper.Map<PetDto>(pet);
        }

        public async Task<Page<PetDto>> GetPetsAsync(int pageNumber, int pageSize)
        {
            if (pageSize <= 0 || pageNumber <= 0)
                throw new DataValidationException("Incorrect page number and(or) size provided!");

            var query = _petRep.GetAll().OrderByDescending(e => e.CreationDate);
            var page = await Page<Pet>.CreateFromQueryAsync(query, pageNumber, pageSize);

            return _mapper.Map<Page<PetDto>>(page);
        }

        public async Task<IQueryable<PetDto>> GetPetsByFarmIdAsync(int farmId)
        {
            var pets = _petRep.GetAll().Where(x => x.Farm.Id == farmId);
            if (pets == null)
                throw new NotFoundException("This farm does not exist!");

            return _mapper.Map<IQueryable<PetDto>>(pets);
        }

        public async Task<bool> RemovePetAsync(int petId)
        {
            if (await _petRep.GetByIdAsync(petId) == null)
                throw new NotFoundException("This pet does not exist!");

            return await _petRep.RemoveAsync(petId);
        }

        public async Task UpdatePetAsync(PetDto pet)
        {
            if (await _petRep.GetByIdAsync(pet.Id) == null)
                throw new NotFoundException("This pet does not exist!");

            await _petRep.UpdateAsync(_mapper.Map<Pet>(pet));
        }
    }
}
