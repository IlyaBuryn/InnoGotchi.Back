using AutoMapper;
using InnoGotchi.BusinessLogic.Dto;
using InnoGotchi.DataAccess.Models;

namespace InnoGotchi.BusinessLogic.MappingProfiles
{
    public class BodyPartMapProfile : Profile
    {
        public BodyPartMapProfile()
        {
            CreateMap<BodyPart, BodyPartDto>().ReverseMap();
        }
    }
}
