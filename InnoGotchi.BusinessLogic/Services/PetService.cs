using AutoMapper;
using FluentValidation;
using InnoGotchi.BusinessLogic.Components;
using InnoGotchi.BusinessLogic.Dto;
using InnoGotchi.BusinessLogic.Exceptions;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.DataAccess.Components;
using InnoGotchi.DataAccess.Interfaces;
using InnoGotchi.DataAccess.Models;

namespace InnoGotchi.BusinessLogic.Services
{
    public class PetService : IPetService
    {
        private readonly IRepository<Pet> _petRep;
        private readonly IRepository<Farm> _farmRep;
        private readonly IValidator<Pet> _petValidator;
        private readonly IMapper _mapper;

        public PetService(IRepository<Pet> petRep,
            IRepository<Farm> farmRep,
            IValidator<Pet> petValidator,
            IMapper mapper)
        {
            _petValidator = petValidator;
            _farmRep = farmRep;
            _petRep = petRep;
            _mapper = mapper;
        }

        public async Task<int?> AddNewPetAsync(PetDto petToAdd)
        {
            var validationResult = await _petValidator.ValidateAsync(_mapper.Map<Pet>(petToAdd));

            if (!validationResult.IsValid)
                throw new DataValidationException();

            var pet = await _petRep.GetOneAsync(x => x.Name == petToAdd.Name);
            if (pet != null)
                throw new DataValidationException("This pet is already exist!");

            var farm = await _farmRep.GetByIdAsync(petToAdd.FarmId);
            if (farm == null)
                throw new NotFoundException(nameof(farm));

            petToAdd.CreationDate = DateTime.Now;

            return await _petRep.AddAsync(_mapper.Map<Pet>(petToAdd));
        }

        public async Task<PetDto?> GetPetByIdAsync(int petId)
        {
            var pet = await _petRep.GetByIdAsync(petId);
            if (pet == null)
                throw new NotFoundException(nameof(pet));

            return _mapper.Map<PetDto>(pet);
        }

        public async Task<Page<PetDto>> GetPetsAsync(int pageNumber, int pageSize)
        {
            if (pageSize <= 0 || pageNumber <= 0)
                throw new DataValidationException("Incorrect page number and(or) size provided!");

            var pets = await _petRep.GetAllAsync(pageNumber, pageSize);
            return _mapper.Map<Page<Pet>, Page<PetDto>>(pets);
        }

        public async Task<IQueryable<PetDto>> GetPetsByFarmIdAsync(int farmId)
        {
            var pets = await _petRep.GetAllAsync(x => x.FarmId == farmId);
            if (pets == null)
                throw new NotFoundException(nameof(Farm));

            return _mapper.Map<IQueryable<PetDto>>(pets);
        }

        public async Task<bool> RemovePetAsync(int petId)
        {
            var pet = await _petRep.GetByIdAsync(petId);
            if (pet == null)
                throw new NotFoundException(nameof(pet));

            return await _petRep.RemoveAsync(petId);
        }

        public async Task<bool> UpdatePetAsync(PetDto petToUpdate)
        {
            var validationResult = await _petValidator.ValidateAsync(_mapper.Map<Pet>(petToUpdate));

            if (!validationResult.IsValid)
                throw new DataValidationException();

            var pet = await _petRep.GetByIdAsync(petToUpdate.Id);
            if (pet == null)
                throw new NotFoundException(nameof(pet));

            var tmp = await _petRep.GetOneAsync(x => x.Name == petToUpdate.Name);
            if (tmp != null)
                throw new DataValidationException("Incorrect pet data!");

            pet.Name = petToUpdate.Name;

            return await _petRep.UpdateAsync(pet);
        }
    }
}
