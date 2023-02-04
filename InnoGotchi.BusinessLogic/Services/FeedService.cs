using AutoMapper;
using FluentValidation;
using InnoGotchi.BusinessLogic.Exceptions;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.Components.DtoModels;
using InnoGotchi.Components.Settings;
using InnoGotchi.DataAccess.Interfaces;
using InnoGotchi.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.BusinessLogic.Services
{
    public enum FeedActionType
    {
        Feed = 1,
        Drink = 2
    }

    public class FeedService : IFeedService
    {
        private readonly IRepository<Feed> _feedRep;
        private readonly IRepository<IdentityUser> _userRep;
        private readonly IRepository<Pet> _petRep;
        private readonly IRepository<VitalSign> _vitalSignRep;
        private readonly IRepository<Farm> _farmRep;
        private readonly IRepository<Collaborator> _collabRep;
        private readonly IValidator<FeedDto> _feedValidator;
        private readonly IMapper _mapper;

        private bool _recalculateHappyDays = true;

        public FeedService(IRepository<Feed> feedRep,
            IRepository<IdentityUser> userRep,
            IRepository<Pet> petRep,
            IValidator<FeedDto> feedValidator, 
            IMapper mapper,
            IRepository<VitalSign> vitalSignRep,
            IRepository<Farm> farmRep,
            IRepository<Collaborator> collabRep)
        {
            _feedRep = feedRep;
            _userRep = userRep;
            _petRep = petRep;
            _feedValidator = feedValidator;
            _mapper = mapper;
            _vitalSignRep = vitalSignRep;
            _farmRep = farmRep;
            _collabRep = collabRep;
        }


        public async Task<int?> FeedPet(FeedDto feedData, FeedActionType feedActionType)
        {
            var validationResult = await _feedValidator.ValidateAsync(feedData);

            if (!validationResult.IsValid)
            {
                throw new DataValidationException();
            }

            var pet = await _petRep.GetByIdAsync(feedData.PetId);
            if (pet == null)
            {
                throw new NotFoundException(nameof(pet));
            }

            var farm = await _farmRep.GetByIdAsync(pet.FarmId);
            if (farm == null)
            {
                throw new NotFoundException(nameof(farm));
            }

            if ((await _collabRep.GetOneAsync(x => x.FarmId == farm.Id && x.IdentityUserId == feedData.IdentityUserId)) == null)
            {
                if (farm.IdentityUserId != feedData.IdentityUserId)
                {
                    throw new DataValidationException("The user does't have access to this pet!");
                }
            }

            var user = await _userRep.GetByIdAsync(feedData.IdentityUserId);
            if (user == null)
            {
                throw new NotFoundException(nameof(user));
            }

            var vitalSign = await _vitalSignRep.GetOneAsync(x => x.PetId == pet.Id);
            if (vitalSign == null)
            {
                throw new NotFoundException(nameof(vitalSign));
            }

            if (!vitalSign.IsAlive)
            {
                throw new DataValidationException("Your pet is dead and cannot be fed!");
            }

            if (feedActionType == FeedActionType.Feed)
            {
                vitalSign.HungerLevel -= feedData.FoodCount;
                if (vitalSign.HungerLevel < 0) vitalSign.HungerLevel = 0;
            }
            if (feedActionType == FeedActionType.Drink)
            {
                vitalSign.ThirstyLevel -= feedData.WaterCount;
                if (vitalSign.ThirstyLevel < 0) vitalSign.ThirstyLevel = 0;
            }

            var updateResult = await _vitalSignRep.UpdateAsync(vitalSign);

            if (updateResult)
            {
                feedData.FeedTime = DateTime.Now;
                return await _feedRep.AddAsync(_mapper.Map<Feed>(feedData));
            }

            return null;
        }

        public double? GetFeedPeriods(int farmId, FeedActionType feedActionType)
        {
            var farm = _farmRep.GetAll(x => x.Id == farmId).Include(x => x.Pets).FirstOrDefault();
            if (farm == null)
            {
                throw new NotFoundException(nameof(farm));
            }

            var pets = farm.Pets.Select(x => x.Id);

            IQueryable<Feed?> feedsInfo = Enumerable.Empty<Feed>().AsQueryable();
            if (feedActionType == FeedActionType.Feed)
            {
                feedsInfo = _feedRep.GetAll(x => pets.Contains(x.PetId)
                    && x.IdentityUserId != null
                    && x.FoodCount > 0);
            }
            else if (feedActionType == FeedActionType.Drink)
            {
                feedsInfo = _feedRep.GetAll(x => pets.Contains(x.PetId)
                    && x.IdentityUserId != null
                    && x.WaterCount > 0);
            }
            else return null;

            var dates = new List<int>();
            foreach (var item in feedsInfo)
            {
                if (item != null)
                {
                    dates.Add((DateTime.Now - item.FeedTime).Hours);
                }
            }

            return Math.Round(dates.Average(), 3);
        }

        public async Task RecalculatePetsNeeds(int farmId)
        {
            var farm = _farmRep.GetAll(x => x.Id == farmId).Include(x => x.Pets).FirstOrDefault();
            if (farm == null)
            {
                throw new NotFoundException(nameof(farm));
            }

            foreach(var pet in farm.Pets)
            {
                var petVitalSign = await _vitalSignRep.GetOneAsync(x => x.PetId == pet.Id);
                if (petVitalSign == null || !petVitalSign.IsAlive)
                {
                    continue;
                }

                var lastFeedTime = _feedRep.GetAll(x => x.PetId == pet.Id && x.FoodCount != 0).FirstOrDefault();
                var lastDrinkTime = _feedRep.GetAll(x => x.PetId == pet.Id && x.WaterCount != 0).FirstOrDefault();

                await RecalculatePetLevels(pet, petVitalSign, new Feed(), 
                    lastFeedTime == null ? new Feed() : lastFeedTime, FeedActionType.Feed);
                await RecalculatePetLevels(pet, petVitalSign, new Feed(), 
                    lastDrinkTime == null ? new Feed() : lastDrinkTime, FeedActionType.Drink);
            }
        }



        private async Task RecalculatePetLevels(Pet pet, VitalSign petVitalSign, Feed newFeedTime, Feed lastFeedTime, FeedActionType feedActionType)
        {
            newFeedTime.PetId = pet.Id;
            int periodCount = await TryMakeFeedTimeForNewPet(pet, newFeedTime, lastFeedTime, feedActionType);

            if (periodCount != 0)
            {
                UpdateVitalSignNeeds(petVitalSign, feedActionType, periodCount);
                await _vitalSignRep.UpdateAsync(petVitalSign);
                return;
            }

            if (lastFeedTime == null)
            {
                return;
            }

            var customDays = (DateTime.Now - lastFeedTime.FeedTime).Hours;
            periodCount = customDays / DefaultSettings.StarvingPeriodInHours;
            newFeedTime.FeedTime = DateTime.Now - TimeSpan.FromHours(customDays % DefaultSettings.StarvingPeriodInHours);
            if (periodCount == 0)
            {
                return;
            }

            UpdateVitalSignNeeds(petVitalSign, feedActionType, periodCount);

            var updateResult = await _vitalSignRep.UpdateAsync(petVitalSign);
            if (updateResult)
            { 
                if (lastFeedTime.IdentityUserId == null)
                {
                    newFeedTime.Id = lastFeedTime.Id;
                    newFeedTime.IdentityUserId = null;
                    await _feedRep.UpdateAsync(newFeedTime);
                }
                else
                {
                    Feed? preLastFeedTime = null;
                    if (feedActionType == FeedActionType.Feed)
                    {
                        preLastFeedTime = _feedRep.GetAll(x => x.PetId == pet.Id
                            && x.FoodCount != 0 && x.IdentityUserId == null).FirstOrDefault();
                    }
                    if (feedActionType == FeedActionType.Drink)
                    {
                        preLastFeedTime = _feedRep.GetAll(x => x.PetId == pet.Id
                            && x.WaterCount != 0 && x.IdentityUserId == null).FirstOrDefault();
                    }

                    if (preLastFeedTime != null)
                    {
                        await _feedRep.RemoveAsync(preLastFeedTime.Id);
                    }

                    DistributeFood(newFeedTime, feedActionType, -periodCount);

                    await _feedRep.AddAsync(newFeedTime);
                }
            }
        }

        private async Task<int> TryMakeFeedTimeForNewPet(Pet pet, Feed newFeedTime, Feed lastFeedTime, FeedActionType feedActionType)
        {
            if (lastFeedTime == null)
            {
                var customDays = (int)(DateTime.Now - pet.CreationDate).TotalHours;
                var periodCount = customDays / DefaultSettings.StarvingPeriodInHours;
                newFeedTime.FeedTime = DateTime.Now - TimeSpan.FromHours(customDays % DefaultSettings.StarvingPeriodInHours);
                newFeedTime.IdentityUserId = null;

                DistributeFood(newFeedTime, feedActionType, (periodCount == 0) ? -1 : -periodCount);

                await _feedRep.AddAsync(newFeedTime);
                return periodCount;
            }

            return 0;
        }

        private void UpdateVitalSignNeeds(VitalSign vitalSign, FeedActionType feedActionType, int periodCount)
        {
            if (_recalculateHappyDays)
            {
                int k = 0; bool flag = true;
                foreach (var item in DefaultSettings.HappyPeriods)
                {
                    if (vitalSign.HungerLevel == item && vitalSign.ThirstyLevel == item)
                    {
                        vitalSign.HappinessDaysCount += DefaultSettings.HappyPeriods.Length - k;
                        flag = false;
                        break;
                    }
                    k++;
                }

                if (DefaultSettings.HappyPeriods.Contains(vitalSign.HungerLevel)
                    && DefaultSettings.HappyPeriods.Contains(vitalSign.ThirstyLevel) && flag)
                {
                    vitalSign.HappinessDaysCount += DefaultSettings.HappyPeriods.Length - 1;
                }

            }
            _recalculateHappyDays = !_recalculateHappyDays;

            DistributeFood(vitalSign, feedActionType, periodCount);
            if (vitalSign.HungerLevel >= DefaultSettings.MaxInclusiveHungerLevel ||
                vitalSign.ThirstyLevel >= DefaultSettings.MaxInclusiveHungerLevel)
            {
                vitalSign.IsAlive = false;
            }
        }

        private void DistributeFood(VitalSign vitalSign, FeedActionType feedActionType, int periodCount)
        {
            if (feedActionType == FeedActionType.Feed)
            {
                vitalSign.HungerLevel += periodCount;
            }
            if (feedActionType == FeedActionType.Drink)
            {
                vitalSign.ThirstyLevel += periodCount;
            }
        }
        private void DistributeFood(Feed newFeedTime, FeedActionType feedActionType, int periodCount)
        {
            if (feedActionType == FeedActionType.Feed)
            {
                newFeedTime.FoodCount = periodCount;
            }
            if (feedActionType == FeedActionType.Drink)
            {
                newFeedTime.WaterCount = periodCount;
            }
        }
    }
}
