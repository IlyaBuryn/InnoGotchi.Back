using AutoMapper;
using FluentValidation;
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
            var vs = _vitalSignRep.GetAll(x => x.PetId == vitalSignToAdd.PetId)
                .FirstOrDefault();
            if (vs != null)
            {
                throw new DataValidationException("A vital sign already exists for this pet!");
            }


            var pet = _petRep.GetAll(x => x.Id == vitalSignToAdd.PetId);
            if (pet == null)
            {
                throw new NotFoundException(nameof(pet));
            }

            return await _vitalSignRep.AddAsync(_mapper.Map<VitalSign>(vitalSignToAdd));
        }

        public VitalSignDto? GetVitalSignByPetId(int petId)
        {
            var vitalSign = _vitalSignRep.GetAll(x => x.Pet.Id == petId)
                .FirstOrDefault();

            if (vitalSign == null)
            {
                throw new NotFoundException(nameof(vitalSign));
            }

            return _mapper.Map<VitalSignDto>(vitalSign);
        }

        public VitalSignDto? GetVitalSignById(int vitalSignId)
        {
            var vitalSign = _vitalSignRep.GetAll(x => x.Id == vitalSignId)
                .FirstOrDefault();

            if (vitalSign == null)
            {
                throw new NotFoundException(nameof(vitalSign));
            }

            return _mapper.Map<VitalSignDto>(vitalSign);
        }

        public async Task<bool> UpdateVitalSignAsync(VitalSignDto vitalSignToUpdate)
        {
            if (await _vitalSignRep.GetByIdAsync(vitalSignToUpdate.Id) == null)
            {
                throw new NotFoundException(nameof(vitalSignToUpdate));
            }

            return await _vitalSignRep.UpdateAsync(_mapper.Map<VitalSign>(vitalSignToUpdate));
        }
    }
}
