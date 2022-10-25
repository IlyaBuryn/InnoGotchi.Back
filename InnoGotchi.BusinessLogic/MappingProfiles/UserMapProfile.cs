using AutoMapper;
using InnoGotchi.BusinessLogic.Dto;
using InnoGotchi.DataAccess.Models;

namespace InnoGotchi.BusinessLogic.MappingProfiles
{
    public class UserMapProfile : Profile
    {
        public UserMapProfile()
        {
            CreateMap<IdentityUser, IdentityUserDto>()
                //.ForMember(c => c.RoleId, option => option.MapFrom(src => src.Role))
            .ReverseMap();
        }
    }
}
