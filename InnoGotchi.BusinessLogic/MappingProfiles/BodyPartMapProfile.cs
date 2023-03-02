using AutoMapper;
using InnoGotchi.Components.DtoModels;
using InnoGotchi.DataAccess.Models;

namespace InnoGotchi.BusinessLogic.MappingProfiles
{
    public class BodyPartMapProfile : Profile
    {
        public BodyPartMapProfile()
        {
            CreateMap<BodyPart, BodyPartDto>()
                .ForMember(dto => dto.BodyPartType, opt => opt.MapFrom(ent => ent.BodyPartType))
                .ReverseMap();
            CreateMap<BodyPartType, BodyPartTypeDto>().ReverseMap();
        }
    }
}
