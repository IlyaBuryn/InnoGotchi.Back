using AutoMapper;
using InnoGotchi.BusinessLogic.Dto;
using InnoGotchi.DataAccess.Models;

namespace InnoGotchi.BusinessLogic.MappingProfiles
{
    public class FarmMapProfile : Profile
    {
        public FarmMapProfile()
        {
            CreateMap<Farm, FarmDto>().ReverseMap();
        }
    }
}
