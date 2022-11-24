using AutoMapper;
using InnoGotchi.Components.DtoModels;
using InnoGotchi.DataAccess.Models;

namespace InnoGotchi.BusinessLogic.MappingProfiles
{
    public class UserMapProfile : Profile
    {
        public UserMapProfile()
        {
            CreateMap<IdentityUser, IdentityUserDto>().ReverseMap();
        }
    }
}
