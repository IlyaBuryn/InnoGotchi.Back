using AutoMapper;
using FluentValidation;
using InnoGotchi.BusinessLogic.Exceptions;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.Components.DtoModels;
using InnoGotchi.Components.Enums;
using InnoGotchi.DataAccess.Components;
using InnoGotchi.DataAccess.Interfaces;
using InnoGotchi.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.BusinessLogic.Services
{
    public class PetService : IPetService
    {
        private readonly IRepository<Pet> _petRep;
        private readonly IRepository<Farm> _farmRep;
        private readonly IRepository<BodyPartPet> _relationRep;
        private readonly IValidator<Pet> _petValidator;
        private readonly IMapper _mapper;

        public PetService(IRepository<Pet> petRep,
            IRepository<Farm> farmRep,
            IRepository<BodyPartPet> relationRep,
            IValidator<Pet> petValidator,
            IMapper mapper)
        {
            _petValidator = petValidator;
            _farmRep = farmRep;
            _petRep = petRep;
            _mapper = mapper;
            _relationRep = relationRep;
        }

        public async Task<int?> AddNewPetAsync(PetDto petToAdd)
        {
            var validationResult = await _petValidator.ValidateAsync(_mapper.Map<Pet>(petToAdd));

            if (!validationResult.IsValid)
                throw new DataValidationException();

            var petExist = await _petRep.GetOneAsync(x => x.Name == petToAdd.Name);
            if (petExist != null)
                throw new DataValidationException("This pet is already exist!");

            var farm = await _farmRep.GetByIdAsync(petToAdd.FarmId);
            if (farm == null)
                throw new NotFoundException(nameof(farm));

            var pet = new Pet()
            {
                Name = petToAdd.Name,
                CreationDate = DateTime.Now,
                FarmId = petToAdd.FarmId,
                VitalSign = new VitalSign()
            };

            var resultId = await _petRep.AddAsync(pet);

            if (resultId != null && resultId != 0)
            {
                foreach (var item in _mapper.Map<ICollection<BodyPart>>(petToAdd.BodyParts))
                    await _relationRep.AddAsync(new BodyPartPet() { BodyPartsId = item.Id, PetsId = (int)resultId });
            }

            return resultId;
        }

        public async Task<PetDto?> GetPetByIdAsync(int petId)
        {
            var pet = (await _petRep.GetAllAsync(x => x.Id == petId))
                .Include(x => x.BodyParts)
                .Include(x => x.VitalSign)
                .FirstOrDefault();

            if (pet == null)
                throw new NotFoundException(nameof(pet));

            return _mapper.Map<PetDto>(pet);
        }

        public async Task<List<PetDto>> GetPetsAsyncAsPage(int pageNumber, int pageSize, SortFilter sortFilter)
        {
            if (pageSize <= 0 || pageNumber <= 0)
                throw new DataValidationException("Incorrect page number and(or) size provided!");

            IQueryable<Pet> pets = await FilterPage(sortFilter);

            var page =  await CreatePage(pets, pageNumber, pageSize);

            return _mapper.Map<List<PetDto>>(page);
        }

        public async Task<int> GetAllPetsCount()
        {
            return (await _petRep.GetAllAsync(x => x.VitalSign.IsAlive)).Count();
        }

        public async Task<List<PetDto>> GetPetsByFarmIdAsync(int farmId)
        {
            var pets = (await _petRep.GetAllAsync(x => x.FarmId == farmId))
                .Include(x => x.BodyParts)
                .Include(x => x.VitalSign);
            if (pets == null)
                throw new NotFoundException(nameof(Farm));

            return _mapper.Map<List<PetDto>>(pets);
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

        public virtual async Task<IOrderedQueryable<Pet>> FilterPage(SortFilter sortFilter)
        {
            switch (sortFilter)
            {
                case SortFilter.ByHappinessDays:
                    return (await _petRep.GetAllAsync(x => x.VitalSign.IsAlive))
                        .Include(x => x.BodyParts)
                        .Include(x => x.VitalSign).OrderByDescending(x => x.VitalSign.HappinessDaysCount);
                    break;
                case SortFilter.ByAge:
                    return (await _petRep.GetAllAsync(x => x.VitalSign.IsAlive))
                        .Include(x => x.BodyParts)
                        .Include(x => x.VitalSign).OrderByDescending(x => x.CreationDate);
                    break;
                case SortFilter.ByHungerLevel:
                    return (await _petRep.GetAllAsync(x => x.VitalSign.IsAlive))
                        .Include(x => x.BodyParts)
                        .Include(x => x.VitalSign).OrderBy(x => x.VitalSign.HungerLevel);
                    break;
                case SortFilter.ByThirstyLevel:
                    return (await _petRep.GetAllAsync(x => x.VitalSign.IsAlive))
                        .Include(x => x.BodyParts)
                        .Include(x => x.VitalSign).OrderBy(x => x.VitalSign.ThirstyLevel);
                    break;
                default:
                    return (await _petRep.GetAllAsync(x => x.VitalSign.IsAlive))
                        .Include(x => x.BodyParts)
                        .Include(x => x.VitalSign).OrderByDescending(x => x.VitalSign.HappinessDaysCount);
                    break;
            }
        }

        public virtual async Task<Page<T>> CreatePage<T>(IQueryable<T> set, int pageNumber, int pageSize) where T : class
        {
            return await Page<T>.CreateFromQueryAsync(set, pageNumber, pageSize);
        }
    }
}
