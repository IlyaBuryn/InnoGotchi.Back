using AutoMapper;
using InnoGotchi.Components.DtoModels;
using InnoGotchi.DataAccess.Models;

namespace InnoGotchi.BusinessLogic.MappingProfiles
{
    public class CollaboratorMapProfile : Profile
    {
        public CollaboratorMapProfile()
        {
            CreateMap<Collaborator, CollaboratorDto>().ReverseMap();
        }
    }
}
