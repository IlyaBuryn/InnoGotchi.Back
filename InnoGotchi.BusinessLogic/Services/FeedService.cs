using AutoMapper;
using FluentValidation;
using InnoGotchi.BusinessLogic.Components;
using InnoGotchi.BusinessLogic.Dto;
using InnoGotchi.BusinessLogic.Exceptions;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.DataAccess.Interfaces;
using InnoGotchi.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.BusinessLogic.Services
{
    public class FeedService : IFeedService
    {
        private readonly IRepository<Feed> _feedRep;
        private readonly IRepository<IdentityUser> _userRep;
        private readonly IRepository<Pet> _petRep;
        private readonly IRepository<VitalSign> _vitalSignRep;
        private readonly IRepository<Farm> _farmRep;
        private readonly IValidator<Feed> _feedValidator;
        private readonly IMapper _mapper;

        public FeedService(IRepository<Feed> feedRep,
            IRepository<IdentityUser> userRep,
            IRepository<Pet> petRep,
            IValidator<Feed> feedValidator, 
            IMapper mapper,
            IRepository<VitalSign> vitalSignRep,
            IRepository<Farm> farmRep)
        {
            _feedRep = feedRep;
            _userRep = userRep;
            _petRep = petRep;
            _feedValidator = feedValidator;
            _mapper = mapper;
            _vitalSignRep = vitalSignRep;
            _farmRep = farmRep;
        }

        public async Task<int?> FeedPet(FeedDto feedData)
        {
            var validationResult = await _feedValidator.ValidateAsync(_mapper.Map<Feed>(feedData));

            if (!validationResult.IsValid)
                throw new DataValidationException();

            var pet = await _petRep.GetByIdAsync(feedData.PetId);
            if (pet == null)
                throw new NotFoundException(nameof(pet));

            var user = await _userRep.GetByIdAsync(feedData.IdentityUserId);
            if (user == null)
                throw new NotFoundException(nameof(user));

            var vitalSign = await _vitalSignRep.GetOneAsync(x => x.PetId == pet.Id);
            if (vitalSign == null)
                throw new NotFoundException(nameof(vitalSign));

            vitalSign.HungerLevel -= feedData.FoodCount;
            if (vitalSign.HungerLevel < 0) vitalSign.HungerLevel = 0;

            vitalSign.ThirsyLevel -= feedData.WaterCount;
            if (vitalSign.ThirsyLevel < 0) vitalSign.ThirsyLevel = 0;

            var updateResult = await _vitalSignRep.UpdateAsync(vitalSign);

            if (updateResult)
            {
                feedData.FeedTime = DateTime.Now;
                return await _feedRep.AddAsync(_mapper.Map<Feed>(feedData));
            }

            return null;
        }

        public async Task RecalculatePetsNeeds(int farmId)
        {
            var farm = (await _farmRep.GetAllAsync(x => x.Id == farmId)).Include(x => x.Pets).FirstOrDefault();
            if (farm == null)
                throw new NotFoundException(nameof(farm));

            foreach(var pet in farm.Pets)
            {
                var petVitalSign = await _vitalSignRep.GetOneAsync(x => x.PetId == pet.Id);
                if (petVitalSign != null)
                {
                    var feedData = new Feed();
                    feedData.PetId = pet.Id;
                    feedData.Pet = pet;

                    var lastFeedTime = (await _feedRep.GetAllAsync(x => x.PetId == pet.Id)).FirstOrDefault();
                    if (lastFeedTime == null)
                    {
                        feedData.FeedTime = DateTime.Now;
                        feedData.WaterCount = 0;
                        feedData.FoodCount = 0;
                        await _feedRep.AddAsync(feedData);
                    }
                    else
                    {
                        var customDays = (DateTime.Now - lastFeedTime.FeedTime).Hours;
                        int periodCounts = customDays / DefaultSettings.StarvingPeriodInHours;

                        foreach (var item in new int[] { 1, 0 })
                        {
                            if (petVitalSign.HungerLevel == item && petVitalSign.ThirsyLevel == item && periodCounts == item + 1)
                            {
                                petVitalSign.HappinessDaysCount += item;
                                break;
                            }
                        }

                        petVitalSign.HungerLevel += periodCounts / 2;
                        petVitalSign.ThirsyLevel += periodCounts / 2;

                        if (petVitalSign.HungerLevel >= DefaultSettings.MaxInclusiveHungerLevel ||
                            petVitalSign.ThirsyLevel >= DefaultSettings.MaxInclusiveHungerLevel)
                        {
                            petVitalSign.IsAlive = false;
                        }

                        var updateResult = await _vitalSignRep.UpdateAsync(petVitalSign);
                        if (updateResult)
                        {
                            feedData.FeedTime = DateTime.Now;
                            await _feedRep.AddAsync(_mapper.Map<Feed>(feedData));
                        }
                    }
                }
            }
        }
    }
}
