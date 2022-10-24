using AutoMapper;
using InnoGotchi.BusinessLogic.Dto;
using InnoGotchi.DataAccess.Models;

namespace InnoGotchi.BusinessLogic.MappingProfiles
{
    public class PetMapProfile : Profile
    {
        public PetMapProfile()
        {
            CreateMap<Pet, PetDto>().ReverseMap();
        }
    }
}
