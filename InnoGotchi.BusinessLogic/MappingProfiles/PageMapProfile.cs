using AutoMapper;
using InnoGotchi.BusinessLogic.Dto;
using InnoGotchi.DataAccess.Components;
using InnoGotchi.DataAccess.Models;

namespace InnoGotchi.BusinessLogic.MappingProfiles
{
    public class PageMapProfile : Profile
    {
        public PageMapProfile()
        {
            //CreateMap(typeof(Page<>), typeof(Page<>)).ReverseMap();
        }
    }
}
