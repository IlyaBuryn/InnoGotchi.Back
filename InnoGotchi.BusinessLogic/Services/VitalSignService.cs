using AutoMapper;
using InnoGotchi.BusinessLogic.Dto;
using InnoGotchi.BusinessLogic.Exceptions;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.DataAccess.Interfaces;
using InnoGotchi.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.BusinessLogic.Services
{
    public class VitalSignService : IVitalSignService
    {
        private readonly IRepository<Pet> _petRep;
        private readonly IRepository<VitalSign> _vitalSignRep;
        private readonly IMapper _mapper;

        public VitalSignService(IRepository<Pet> petRep, IRepository<VitalSign> vitalSignRep, IMapper mapper)
        {
            _petRep = petRep;
            _vitalSignRep = vitalSignRep;
            _mapper = mapper;
        }

        public async Task<int?> CreateVitalSignAsync(VitalSignDto vitalSign)
        {
            if (vitalSign == null)
                throw new NullReferenceException(nameof(vitalSign));

            var pet = _petRep.GetAll().FirstOrDefault(x => x.Id == vitalSign.PetId);
            if (pet == null)
                throw new NotFoundException("Pet for this vital signs does not exist!");

            return await _vitalSignRep.AddAsync(_mapper.Map<VitalSign>(vitalSign));
        }

        public async Task<PetDto?> GetPetByIdAsync(int petId)
        {
            var vitalSignWithPet = _vitalSignRep.GetAll().Include(x => x.Pet)
                .FirstOrDefault(x => x.Pet.Id == petId);

            if (vitalSignWithPet == null)
                throw new NotFoundException("This pet does not exist!");

            return _mapper.Map<PetDto>(vitalSignWithPet.Pet);
        }

        public async Task<VitalSignDto?> GetVitalSignByIdAsync(int vitalSignId)
        {
            var vitalSign = _vitalSignRep.GetAll().Include(x => x.Pet)
                .FirstOrDefault(x => x.Id == vitalSignId);

            if (vitalSign == null)
                throw new NotFoundException("This vital sign does not exist!");

            return _mapper.Map<VitalSignDto>(vitalSign);
        }

        public async Task UpdateVitalSignAsync(VitalSignDto vitalSign)
        {
            if (await _vitalSignRep.GetByIdAsync(vitalSign.Id) == null)
                throw new NotFoundException("This vital sign does not exist!");

            await _vitalSignRep.UpdateAsync(_mapper.Map<VitalSign>(vitalSign));
        }
    }
}
