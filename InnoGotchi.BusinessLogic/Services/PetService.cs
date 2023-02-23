using AutoMapper;
using InnoGotchi.BusinessLogic.Exceptions;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.Components.DtoModels;
using InnoGotchi.Components.Enums;
using InnoGotchi.DataAccess.Interfaces;
using InnoGotchi.DataAccess.Models;
using System.Linq.Expressions;

namespace InnoGotchi.BusinessLogic.Services
{
    public class PetService : IPetService
    {
        private readonly IRepository<Pet> _petRep;
        private readonly IRepository<Farm> _farmRep;
        private readonly IRepository<BodyPartPet> _bodyPartPetRep;
        private readonly IMapper _mapper;

        public PetService(IRepository<Pet> petRep,
            IRepository<Farm> farmRep,
            IRepository<BodyPartPet> bodyPartPetRep,
            IMapper mapper)
        {
            _farmRep = farmRep;
            _petRep = petRep;
            _mapper = mapper;
            _bodyPartPetRep = bodyPartPetRep;
        }

        public async Task<int?> AddNewPetAsync(PetDto petToAdd)
        {
            var petExist = await _petRep.GetOneAsync(x => x.Name == petToAdd.Name);
            if (petExist != null)
            {
                throw new DataValidationException("This pet is already exist!");
            }

            var farm = await _farmRep.GetOneByIdAsync(petToAdd.FarmId);
            if (farm == null)
            {
                throw new NotFoundException(nameof(farm));
            }

            var pet = _mapper.Map<Pet>(petToAdd);
            pet.VitalSign.IsAlive = true;
            pet.BodyParts = null;

            var resultId = await _petRep.AddAsync(pet);

            if (resultId != null && resultId != 0)
            {
                foreach (var item in _mapper.Map<ICollection<BodyPart>>(petToAdd.BodyParts))
                {
                    await _bodyPartPetRep.AddAsync(
                        new BodyPartPet() 
                        { 
                            BodyPartsId = item.Id, 
                            PetsId = (int)resultId 
                        });
                }
            }

            return resultId;
        }

        public async Task<PetDto?> GetPetByIdAsync(int petId)
        {
            var pet = await _petRep.GetOneByIdAsync(petId, x => x.BodyParts, x => x.VitalSign);
            if (pet == null)
            {
                throw new NotFoundException(nameof(pet));
            }

            return _mapper.Map<PetDto>(pet);
        }

        public async Task<List<PetDto>> GetPetsAsPageAsync(int pageNumber, int pageSize, SortFilter sortFilter)
        {
            if (pageSize <= 0 || pageNumber <= 0)
            {
                throw new DataValidationException("Incorrect page number and(or) size provided!");
            }

            var pets = await FilterPageAsync(sortFilter);

            var page =  await _petRep.GetPageAsync(pets, pageNumber, pageSize);

            return _mapper.Map<List<PetDto>>(page);
        }

        public async Task<int> GetAllPetsCountAsync()
        {
            return await _petRep.GetCountAsync(x => x.VitalSign.IsAlive);
        }

        public async Task<List<PetDto>> GetPetsByFarmIdAsync(int farmId)
        {
            var pets = await _petRep.GetManyAsync(
                expression: x => x.FarmId == farmId,
                includeProperties: new Expression<Func<Pet, object>>[] { x => x.BodyParts, x => x.VitalSign });

            if (pets == null)
            {
                throw new NotFoundException(nameof(Farm));
            }

            return _mapper.Map<List<PetDto>>(pets);
        }

        public async Task<bool> RemovePetAsync(int petId)
        {
            var pet = await _petRep.GetOneByIdAsync(petId);
            if (pet == null)
            {
                throw new NotFoundException(nameof(pet));
            }

            return await _petRep.RemoveAsync(petId);
        }

        public async Task<bool> UpdatePetAsync(PetDto petToUpdate)
        {
            var pet = await _petRep.GetOneByIdAsync(petToUpdate.Id);
            if (pet == null)
            {
                throw new NotFoundException(nameof(pet));
            }

            var petName = await _petRep.GetOneAsync(x => x.Name == petToUpdate.Name);
            if (petName != null)
            {
                throw new DataValidationException("Incorrect pet data!");
            }

            pet.Name = petToUpdate.Name;

            return await _petRep.UpdateAsync(pet);
        }

        private async Task<IQueryable<Pet>> FilterPageAsync(SortFilter sortFilter)
        {
            var includeProperties = new Expression<Func<Pet, object>>[] { x => x.BodyParts, x => x.VitalSign };
            Expression<Func<Pet, bool>> expression = x => x.VitalSign.IsAlive;

            switch (sortFilter)
            {
                case SortFilter.ByHappinessDays:
                    return await _petRep.GetManyAsync(
                        expression, orderBy: x => x.VitalSign.HappinessDaysCount, includeProperties);

                case SortFilter.ByAge:
                    return await _petRep.GetManyAsync(
                        expression, orderBy: x => x.CreationDate, includeProperties);

                case SortFilter.ByHungerLevel:
                    return await _petRep.GetManyAsync(
                        expression, orderBy: x => x.VitalSign.HungerLevel, includeProperties);

                case SortFilter.ByThirstyLevel:
                    return await _petRep.GetManyAsync(
                        expression, orderBy: x => x.VitalSign.ThirstyLevel, includeProperties);

                default:
                    return await _petRep.GetManyAsync(
                        expression, orderBy: x => x.VitalSign.HappinessDaysCount, includeProperties);
            }
        }
    }
}
