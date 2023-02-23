using AutoMapper;
using InnoGotchi.BusinessLogic.Exceptions;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.Components.DtoModels;
using InnoGotchi.DataAccess.Interfaces;
using InnoGotchi.DataAccess.Models;

namespace InnoGotchi.BusinessLogic.Services
{
    public class BodyPartService : IBodyPartService
    {
        private readonly IRepository<BodyPart> _bodyPartRep;
        private readonly IRepository<BodyPartType> _bodyPartTypeRep;
        private readonly IRepository<Pet> _petRep;
        private readonly IMapper _mapper;

        public BodyPartService(IRepository<BodyPart> bodyPartRep,
            IRepository<BodyPartType> bodyPartTypeRep,
            IRepository<Pet> petRep,
            IMapper mapper)
        {
            _bodyPartRep = bodyPartRep;
            _bodyPartTypeRep = bodyPartTypeRep;
            _petRep = petRep;
            _mapper = mapper;
        }

        public async Task<int?> AddNewBodyPartAsync(BodyPartDto bodyPartToAdd)
        {
            var bodyPartType = await _bodyPartTypeRep.GetOneByIdAsync(bodyPartToAdd.BodyPartTypeId);
            if (bodyPartType == null)
            {
                throw new NotFoundException(nameof(bodyPartType));
            }

            var bodyPart = _mapper.Map<BodyPart>(bodyPartToAdd);
            return await _bodyPartRep.AddAsync(bodyPart);
        }

        public async Task<bool> RemoveBodyPartAsync(int bodyPartId)
        {
            var bodyPart = await _bodyPartRep.GetOneByIdAsync(bodyPartId);
            if (bodyPart == null)
            {
                throw new NotFoundException(nameof(bodyPart));
            }

            return await _bodyPartRep.RemoveAsync(bodyPartId);
        }

        public async Task<bool> UpdateBodyPartAsync(BodyPartDto bodyPartToUpdate)
        {
            var bodyPart = await _bodyPartRep.GetOneByIdAsync(bodyPartToUpdate.Id);
            if (bodyPart == null)
            {
                throw new NotFoundException(nameof(bodyPart));
            }

            var bodyPartType = await _bodyPartTypeRep.GetOneByIdAsync(bodyPartToUpdate.BodyPartTypeId);
            if (bodyPartType == null)
            {
                throw new NotFoundException(nameof(bodyPartType));
            }

            bodyPart.BodyPartTypeId = bodyPartType.Id;
            bodyPart.Image = bodyPartToUpdate.Image;
            bodyPart.Color = bodyPartToUpdate.Color;

            return await _bodyPartRep.UpdateAsync(bodyPart);
        }

        public async Task<BodyPartDto?> GetBodyPartByIdAsync(int bodyPartId)
        {
            var bodyPart = await _bodyPartRep.GetOneByIdAsync(bodyPartId);
            if (bodyPart == null)
            {
                throw new NotFoundException(nameof(bodyPart));
            }

            return _mapper.Map<BodyPartDto?>(bodyPart);
        }

        public async Task<List<BodyPartDto>> GetBodyPartsAsync()
        {
            var bodyParts = await _bodyPartRep.GetAllAsync(x => x.BodyPartType);
            return _mapper.Map<List<BodyPartDto>>(bodyParts);

        }

        public async Task<List<BodyPartDto>> GetBodyPartsByPetIdAsync(int petId)
        {
            var bodyParts = await _bodyPartRep.GetManyByIdAsync(petId, x => x.BodyPartType);
            if (bodyParts == null)
            {
                throw new NotFoundException(nameof(BodyPart));
            }

            return _mapper.Map<List<BodyPartDto>>(bodyParts);
        }

        public async Task<List<BodyPartDto>> GetBodyPartsByTypeIdAsync(int bodyPartTypeId)
        {
            var bodyParts = await _bodyPartRep.GetManyAsync(
                expression: x => x.BodyPartTypeId == bodyPartTypeId, 
                includeProperties: x => x.BodyPartType);

            if (bodyParts == null)
            {
                throw new NotFoundException(nameof(BodyPart));
            }

            return _mapper.Map<List<BodyPartDto>>(bodyParts);
        }


        public async Task<int?> CreateBodyPartTypeAsync(BodyPartTypeDto bodyPartTypeToAdd)
        {
            var bodyPartType = _mapper.Map<BodyPartType>(bodyPartTypeToAdd);
            return await _bodyPartTypeRep.AddAsync(bodyPartType);
        }

        public async Task<bool> DeleteBodyPartTypeAsync(int bodyPartTypeId)
        {
            var bpType = _bodyPartTypeRep.GetOneByIdAsync(bodyPartTypeId);
            if (bpType == null)
            {
                throw new NotFoundException(nameof(bpType));
            }

            return await _bodyPartTypeRep.RemoveAsync(bodyPartTypeId);
        }

    }
}
