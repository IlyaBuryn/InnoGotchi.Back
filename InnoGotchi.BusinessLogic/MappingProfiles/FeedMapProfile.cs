using AutoMapper;
using InnoGotchi.BusinessLogic.Dto;
using InnoGotchi.DataAccess.Models;

namespace InnoGotchi.BusinessLogic.MappingProfiles
{
    public class FeedMapProfile : Profile
    {
        public FeedMapProfile()
        {
            CreateMap<Feed, FeedDto>().ReverseMap();
        }
    }
}
