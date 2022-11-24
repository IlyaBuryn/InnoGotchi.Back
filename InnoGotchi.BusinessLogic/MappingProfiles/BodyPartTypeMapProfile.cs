using AutoMapper;
using InnoGotchi.Components.DtoModels;
using InnoGotchi.DataAccess.Models;

namespace InnoGotchi.BusinessLogic.MappingProfiles
{
    public class BodyPartTypeMapProfile : Profile
    {
        public BodyPartTypeMapProfile()
        {
            CreateMap<BodyPartType, BodyPartTypeDto>().ReverseMap();
        }
    }
}
