using AutoMapper;
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
        private readonly IRepository<Collaborator> _collaboratorRep;
        private readonly IMapper _mapper;

        private bool _shouldUpdateVitalSigns;

        public FeedService(IRepository<Feed> feedRep,
            IRepository<IdentityUser> userRep,
            IRepository<Pet> petRep,
            IMapper mapper,
            IRepository<VitalSign> vitalSignRep,
            IRepository<Farm> farmRep,
            IRepository<Collaborator> collaboratorRep)
        {
            _feedRep = feedRep;
            _userRep = userRep;
            _petRep = petRep;
            _mapper = mapper;
            _vitalSignRep = vitalSignRep;
            _farmRep = farmRep;
            _collaboratorRep = collaboratorRep;
        }

        private class ValuePair
        {
            public ValuePair(int feedingUpdateCycles, Feed? lastFeeding)
            {
                FeedingUpdateCycles = feedingUpdateCycles;
                LastFeeding = lastFeeding;
            }

            public int FeedingUpdateCycles { get; set; }
            public Feed? LastFeeding { get; set; }
        }

        public async Task<int?> FeedPetAsync(FeedDto feedData, FeedActionType feedActionType)
        {
            var pet = await _petRep.GetOneByIdAsync(feedData.PetId);
            if (pet == null)
            {
                throw new NotFoundException(nameof(pet));
            }

            var farm = await _farmRep.GetOneByIdAsync(pet.FarmId);
            if (farm == null)
            {
                throw new NotFoundException(nameof(farm));
            }

            var collaborator = await _collaboratorRep.GetOneAsync(x => x.FarmId == farm.Id && x.IdentityUserId == feedData.IdentityUserId);
            if (collaborator == null && farm.IdentityUserId != feedData.IdentityUserId)
            {
                throw new DataValidationException("The user does't have access to this pet!");
            }

            var user = await _userRep.GetOneByIdAsync(feedData.IdentityUserId);
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
                if (vitalSign.HungerLevel < 0)
                {
                    vitalSign.HungerLevel = 0;
                }
            }

            if (feedActionType == FeedActionType.Drink)
            {
                vitalSign.ThirstyLevel -= feedData.WaterCount;
                if (vitalSign.ThirstyLevel < 0)
                {
                    vitalSign.ThirstyLevel = 0;
                }
            }

            var updateResult = await _vitalSignRep.UpdateAsync(vitalSign);

            if (updateResult)
            {
                feedData.FeedTime = DateTime.Now;
                return await _feedRep.AddAsync(_mapper.Map<Feed>(feedData));
            }

            return null;
        }

        public async Task<double?> GetFeedPeriodsAsync(int farmId, FeedActionType feedActionType)
        {
            var farm = await _farmRep.GetOneByIdAsync(farmId, x => x.Pets);
            if (farm == null)
            {
                throw new NotFoundException(nameof(farm));
            }

            var pets = farm.Pets.Select(x => x.Id);

            IQueryable<Feed?> feedsInfo = Enumerable.Empty<Feed>().AsQueryable();
            if (feedActionType == FeedActionType.Feed)
            {
                feedsInfo = await _feedRep.GetAllAsync(
                    x => pets.Contains(x.PetId)
                    && x.IdentityUserId != null
                    && x.FoodCount > 0);
            }
            else if (feedActionType == FeedActionType.Drink)
            {
                feedsInfo = await _feedRep.GetAllAsync(
                    x => pets.Contains(x.PetId)
                    && x.IdentityUserId != null
                    && x.WaterCount > 0);
            }
            else
            {
                return null;
            }

            var dates = new List<int>();

            if (feedsInfo.Count() == 0)
            {
                return 0;
            }

            foreach (var item in feedsInfo)
            {
                dates.Add((DateTime.Now - item.FeedTime).Hours);
            }

            return Math.Round(dates.Average(), 3);
        }

        public async Task RecalculateVitalSignsAsync(int farmId)
        {
            _shouldUpdateVitalSigns = false;
            var farm = await _farmRep.GetOneByIdAsync(farmId, x => x.Pets);
            if (farm == null)
            {
                throw new NotFoundException(nameof(farm));
            }

            var pets = await _petRep.GetManyAsync(
                expression: x => x.FarmId == farmId,
                includeProperties: x => x.VitalSign);

            if (pets == null || pets.Count() == 0)
            {
                return;
            }

            foreach(var pet in pets)
            {
                var petVitalSign = pet.VitalSign;
                if (petVitalSign == null || !petVitalSign.IsAlive)
                {
                    continue;
                }

                var feedingCycles = new Dictionary<FeedActionType, ValuePair>();

                var lastFeedTime = await _feedRep.GetOneAsync(x => x.PetId == pet.Id && x.FoodCount != 0);
                feedingCycles.Add(FeedActionType.Feed, new ValuePair(GetFeedingCycles(pet, lastFeedTime), lastFeedTime));

                var lastDrinkTime = await _feedRep.GetOneAsync(x => x.PetId == pet.Id && x.WaterCount != 0);
                feedingCycles.Add(FeedActionType.Drink, new ValuePair(GetFeedingCycles(pet, lastDrinkTime), lastDrinkTime));

                TryToUpdateVitalSigns(feedingCycles, pet);

                if (_shouldUpdateVitalSigns)
                {
                    UpdateHappinessDaysCount(feedingCycles, pet);
                    await UpdateFeedInfoAsync(feedingCycles, pet);

                    var vitalSignToUpdate = pet.VitalSign;
                    await _vitalSignRep.UpdateAsync(vitalSignToUpdate);
                }
            }
        }

        private async Task UpdateFeedInfoAsync(Dictionary<FeedActionType, ValuePair> feedingCycles, Pet pet)
        {
            foreach (var cycle in feedingCycles)
            {
                var feedInfo = new Feed()
                {
                    FeedTime = DateTime.Now,
                    FoodCount = 0,
                    WaterCount = 0,
                    IdentityUser = null,
                    Pet = pet,
                };

                if (cycle.Key == FeedActionType.Feed)
                {
                    feedInfo.FoodCount = cycle.Value.FeedingUpdateCycles;
                }

                if (cycle.Key == FeedActionType.Drink)
                {
                    feedInfo.WaterCount = cycle.Value.FeedingUpdateCycles;
                }

                if (cycle.Value.LastFeeding == null)
                {
                    await _feedRep.AddAsync(feedInfo);
                }
                else
                {
                    await _feedRep.UpdateAsync(feedInfo);
                }
            }
        }

        private void UpdateHappinessDaysCount(Dictionary<FeedActionType, ValuePair> feedingCycles, Pet pet)
        {
            int happyPeriodIndex = 0;
            bool isMatchFound = true;

            foreach (var item in DefaultSettings.HappyPeriods)
            {
                if (pet.VitalSign.HungerLevel == item && pet.VitalSign.ThirstyLevel == item)
                {
                    pet.VitalSign.HappinessDaysCount += DefaultSettings.HappyPeriods.Length - happyPeriodIndex;
                    isMatchFound = false;
                    break;
                }
                happyPeriodIndex++;
            }

            if (DefaultSettings.HappyPeriods.Contains(pet.VitalSign.HungerLevel)
                && DefaultSettings.HappyPeriods.Contains(pet.VitalSign.ThirstyLevel) 
                && isMatchFound)
            {
                pet.VitalSign.HappinessDaysCount += DefaultSettings.HappyPeriods.Length - 1;
            }
        }

        private void TryToUpdateVitalSigns(Dictionary<FeedActionType, ValuePair> feedingCycles, Pet pet)
        {
            foreach(var cycle in feedingCycles)
            {
                if (cycle.Value.FeedingUpdateCycles < 1)
                {
                    continue;
                }

                _shouldUpdateVitalSigns = true;
                switch (cycle.Key)
                {
                    case FeedActionType.Feed:
                        pet.VitalSign.HungerLevel += cycle.Value.FeedingUpdateCycles;
                        break;
                    case FeedActionType.Drink:
                        pet.VitalSign.ThirstyLevel += cycle.Value.FeedingUpdateCycles;
                        break;
                    default:
                        break;
                }
            }

            if (pet.VitalSign.HungerLevel >= DefaultSettings.MaxInclusiveHungerLevel
            || pet.VitalSign.ThirstyLevel >= DefaultSettings.MaxInclusiveHungerLevel)
            {
                pet.VitalSign.IsAlive = false;
            }
        }

        private int GetFeedingCycles(Pet pet, Feed? lastFeedTime)
        {
            var feedingCycles = lastFeedTime == null
                ? (int)Math.Floor((DateTime.Now - pet.CreationDate).TotalHours / DefaultSettings.StarvingPeriodInHours)
                : (int)Math.Floor((DateTime.Now - lastFeedTime.FeedTime).TotalHours / DefaultSettings.StarvingPeriodInHours);

            return feedingCycles;
        }
    }
}
