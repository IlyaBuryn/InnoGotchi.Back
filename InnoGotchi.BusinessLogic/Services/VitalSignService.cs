using AutoMapper;
using FluentValidation;
using InnoGotchi.BusinessLogic.Dto;
using InnoGotchi.BusinessLogic.Exceptions;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.DataAccess.Interfaces;
using InnoGotchi.DataAccess.Models;

namespace InnoGotchi.BusinessLogic.Services
{
    public class VitalSignService : IVitalSignService
    {
        private readonly IRepository<Pet> _petRep;
        private readonly IRepository<VitalSign> _vitalSignRep;
        private readonly IMapper _mapper;
        private readonly IValidator<VitalSign> _vsValidator;

        public VitalSignService(IRepository<Pet> petRep,
            IRepository<VitalSign> vitalSignRep,
            IValidator<VitalSign> vsValidator,
            IMapper mapper)
        {
            _petRep = petRep;
            _vitalSignRep = vitalSignRep;
            _vsValidator = vsValidator;
            _mapper = mapper;
        }

        public async Task<int?> CreateVitalSignAsync(VitalSignDto vitalSignToAdd)
        {
            var validationResult = await _vsValidator.ValidateAsync(_mapper.Map<VitalSign>(vitalSignToAdd));
            
            if (!validationResult.IsValid)
                throw new DataValidationException();

            var vs = (await _vitalSignRep.GetAllAsync(x => x.PetId == vitalSignToAdd.PetId))
                .FirstOrDefault();
            if (vs != null)
                throw new DataValidationException("A vital sign already exists for this pet!");


            var pet = await _petRep.GetAllAsync(x => x.Id == vitalSignToAdd.PetId);
            if (pet == null)
                throw new NotFoundException(nameof(pet));

            return await _vitalSignRep.AddAsync(_mapper.Map<VitalSign>(vitalSignToAdd));
        }

        public async Task<VitalSignDto?> GetVitalSignByPetIdAsync(int petId)
        {
            var vitalSign = (await _vitalSignRep.GetAllAsync(x => x.Pet.Id == petId))
                .FirstOrDefault();

            if (vitalSign == null)
                throw new NotFoundException(nameof(vitalSign));

            return _mapper.Map<VitalSignDto>(vitalSign);
        }

        public async Task<VitalSignDto?> GetVitalSignByIdAsync(int vitalSignId)
        {
            var vitalSign = (await _vitalSignRep.GetAllAsync(x => x.Id == vitalSignId))
                .FirstOrDefault();

            if (vitalSign == null)
                throw new NotFoundException(nameof(vitalSign));

            return _mapper.Map<VitalSignDto>(vitalSign);
        }

        public async Task<bool> UpdateVitalSignAsync(VitalSignDto vitalSignToUpdate)
        {
            var validationResult = await _vsValidator.ValidateAsync(_mapper.Map<VitalSign>(vitalSignToUpdate));

            if (!validationResult.IsValid)
                throw new DataValidationException();

            if (await _vitalSignRep.GetByIdAsync(vitalSignToUpdate.Id) == null)
                throw new NotFoundException(nameof(vitalSignToUpdate));

            return await _vitalSignRep.UpdateAsync(_mapper.Map<VitalSign>(vitalSignToUpdate));
        }
    }
}
