using AutoMapper;
using InnoGotchi.Components.DtoModels;
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
