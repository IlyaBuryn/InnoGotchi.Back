using AutoMapper;
using InnoGotchi.BusinessLogic.Exceptions;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.Components.DtoModels;
using InnoGotchi.DataAccess.Interfaces;
using InnoGotchi.DataAccess.Models;

namespace InnoGotchi.BusinessLogic.Services
{
    public class VitalSignService : IVitalSignService
    {
        private readonly IRepository<Pet> _petRep;
        private readonly IRepository<VitalSign> _vitalSignRep;
        private readonly IMapper _mapper;

        public VitalSignService(IRepository<Pet> petRep,
            IRepository<VitalSign> vitalSignRep,
            IMapper mapper)
        {
            _petRep = petRep;
            _vitalSignRep = vitalSignRep;
            _mapper = mapper;
        }

        public async Task<int?> CreateVitalSignAsync(VitalSignDto vitalSignToAdd)
        {
            var vitalSign = await _vitalSignRep.GetOneAsync(x => x.PetId == vitalSignToAdd.PetId);
            if (vitalSign != null)
            {
                throw new DataValidationException("A vital sign already exists for this pet!");
            }


            var pet = await _petRep.GetManyByIdAsync(vitalSignToAdd.PetId);
            if (pet == null)
            {
                throw new NotFoundException(nameof(pet));
            }

            var mappedVitalSign = _mapper.Map<VitalSign>(vitalSignToAdd);
            return await _vitalSignRep.AddAsync(mappedVitalSign);
        }

        public async Task<VitalSignDto?> GetVitalSignByPetIdAsync(int petId)
        {
            var vitalSign = await _vitalSignRep.GetOneAsync(x => x.Pet.Id == petId);
            if (vitalSign == null)
            {
                throw new NotFoundException(nameof(vitalSign));
            }

            return _mapper.Map<VitalSignDto>(vitalSign);
        }

        public async Task<VitalSignDto?> GetVitalSignByIdAsync(int vitalSignId)
        {
            var vitalSign = await _vitalSignRep.GetOneAsync(x => x.Id == vitalSignId);
            if (vitalSign == null)
            {
                throw new NotFoundException(nameof(vitalSign));
            }

            return _mapper.Map<VitalSignDto>(vitalSign);
        }

        public async Task<bool> UpdateVitalSignAsync(VitalSignDto vitalSignToUpdate)
        {
            var vitalSign = await _vitalSignRep.GetOneByIdAsync(vitalSignToUpdate.Id);
            if (vitalSign == null)
            {
                throw new NotFoundException(nameof(vitalSignToUpdate));
            }

            var mappedVitalSign = _mapper.Map<VitalSign>(vitalSignToUpdate);
            return await _vitalSignRep.UpdateAsync(mappedVitalSign);
        }
    }
}
