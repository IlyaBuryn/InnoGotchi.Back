using AutoMapper;
using InnoGotchi.BusinessLogic.Dto;
using InnoGotchi.DataAccess.Models;

namespace InnoGotchi.BusinessLogic.MappingProfiles
{
    public class RoleMapProfile : Profile
    {
        public RoleMapProfile()
        {
            CreateMap<IdentityRole, IdentityRoleDto>().ReverseMap();
        }
    }
}
