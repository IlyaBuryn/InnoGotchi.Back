using AutoMapper;
using InnoGotchi.Components.DtoModels;
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
