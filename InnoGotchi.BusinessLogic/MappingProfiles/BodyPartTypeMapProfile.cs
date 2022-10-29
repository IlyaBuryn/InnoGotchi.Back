using AutoMapper;
using InnoGotchi.BusinessLogic.Dto;
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
