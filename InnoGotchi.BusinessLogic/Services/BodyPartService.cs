using AutoMapper;
using FluentValidation;
using InnoGotchi.BusinessLogic.Exceptions;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.Components.DtoModels;
using InnoGotchi.DataAccess.Interfaces;
using InnoGotchi.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.BusinessLogic.Services
{
    public class BodyPartService : IBodyPartService
    {
        private readonly IRepository<BodyPart> _bpRep;
        private readonly IRepository<BodyPartType> _bpTypeRep;
        private readonly IRepository<Pet> _petRep;
        private readonly IValidator<BodyPart> _bpValidator;
        private readonly IMapper _mapper;

        public BodyPartService(IRepository<BodyPart> bpRep,
            IRepository<BodyPartType> bpTypeRep,
            IRepository<Pet> petRep,
            IValidator<BodyPart> bpValidator,
            IMapper mapper)
        {
            _bpRep = bpRep;
            _bpTypeRep = bpTypeRep;
            _petRep = petRep;
            _bpValidator = bpValidator;
            _mapper = mapper;
        }

        public async Task<int?> AddNewBodyPartAsync(BodyPartDto bodyPartToAdd)
        {
            var validationResult = await _bpValidator
                .ValidateAsync(_mapper.Map<BodyPart>(bodyPartToAdd));

            if (!validationResult.IsValid)
                throw new DataValidationException();

            var type = await _bpTypeRep.GetByIdAsync(bodyPartToAdd.BodyPartTypeId);
            if (type == null)
                throw new NotFoundException(nameof(type));

            return await _bpRep.AddAsync(_mapper.Map<BodyPart>(bodyPartToAdd));
        }

        public async Task<bool> RemoveBodyPartAsync(int bodyPartId)
        {
            var bodyPart = await _bpRep.GetByIdAsync(bodyPartId);
            if (bodyPart == null)
                throw new NotFoundException(nameof(bodyPart));

            return await _bpRep.RemoveAsync(bodyPartId);
        }

        public async Task<bool> UpdateBodyPartAsync(BodyPartDto bodyPartToUpdate)
        {
            var validationResult = await _bpValidator
                .ValidateAsync(_mapper.Map<BodyPart>(bodyPartToUpdate));

            if (!validationResult.IsValid)
                throw new DataValidationException();

            var bodyPart = await _bpRep.GetByIdAsync(bodyPartToUpdate.Id);
            if (bodyPart == null)
                throw new NotFoundException(nameof(bodyPart));

            var type = await _bpTypeRep.GetByIdAsync(bodyPartToUpdate.BodyPartTypeId);
            if (type == null)
                throw new NotFoundException(nameof(type));

            bodyPart.BodyPartTypeId = type.Id;
            bodyPart.Image = bodyPartToUpdate.Image;
            bodyPart.Color = bodyPartToUpdate.Color;

            return await _bpRep.UpdateAsync(bodyPart);
        }

        public async Task<BodyPartDto?> GetBodyPartByIdAsync(int bodyPartId)
        {
            var bodyPart = await _bpRep.GetByIdAsync(bodyPartId);
            if (bodyPart == null)
                throw new NotFoundException(nameof(bodyPart));

            return _mapper.Map<BodyPartDto?>(bodyPart);
        }

        public async Task<List<BodyPartDto>> GetBodyPartsAsync()
        {
            var bparts = (await _bpRep.GetAllAsync()).Include(x => x.BodyPartType);
            return _mapper.Map<List<BodyPartDto>>(bparts);

        }

        public async Task<List<BodyPartDto>> GetBodyPartsByPetIdAsync(int petId)
        {
            var bodyParts = (await _bpRep.GetAllAsync(x => x.Pets.All(p => p.Id == petId)))
                .Include(x => x.BodyPartType);
            if (bodyParts == null)
                throw new NotFoundException(nameof(BodyPart));

            return _mapper.Map<List<BodyPartDto>>(bodyParts);
        }

        public async Task<List<BodyPartDto>> GetBodyPartsByTypeIdAsync(int typeId)
        {
            var bodyParts = (await _bpRep.GetAllAsync(x => x.BodyPartTypeId == typeId))
                .Include(x => x.BodyPartType);
            if (bodyParts == null)
                throw new NotFoundException(nameof(BodyPart));

            return _mapper.Map<List<BodyPartDto>>(bodyParts);
        }






        public async Task<int?> CreateBodyPartTypeAsync(BodyPartTypeDto bodyPartType)
        {
            return await _bpTypeRep.AddAsync(_mapper.Map<BodyPartType>(bodyPartType));
        }

        public async Task<bool> DeleteBodyPartTypeAsync(int bodyPartTypeId)
        {
            var bpType = _bpTypeRep.GetByIdAsync(bodyPartTypeId);
            if (bpType == null)
                throw new NotFoundException(nameof(bpType));

            return await _bpTypeRep.RemoveAsync(bodyPartTypeId);
        }

    }
}
