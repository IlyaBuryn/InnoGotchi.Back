using AutoMapper;
using InnoGotchi.BusinessLogic.Dto;
using InnoGotchi.DataAccess.Models;

namespace InnoGotchi.BusinessLogic.MappingProfiles
{
    public class PetMapProfile : Profile
    {
        public PetMapProfile()
        {
            CreateMap<Pet, PetDto>()    
                .ForMember(dto => dto.BodyParts, opt => opt.MapFrom(ent => ent.BodyParts))
                .ForMember(dto => dto.VitalSign, opt => opt.MapFrom(ent => ent.VitalSign))
                .ReverseMap();

            CreateMap<BodyPart, BodyPartDto>().ReverseMap();
            CreateMap<VitalSign, VitalSignDto>().ReverseMap();
        }
    }
}
