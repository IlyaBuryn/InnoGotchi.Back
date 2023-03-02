using AutoMapper;
using InnoGotchi.Components.DtoModels;
using InnoGotchi.DataAccess.Models;

namespace InnoGotchi.BusinessLogic.MappingProfiles
{
    public class VitalSignMapProfile : Profile
    {
        public VitalSignMapProfile()
        {
            CreateMap<VitalSign, VitalSignDto>().ReverseMap();
        }
    }
}
