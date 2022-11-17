using InnoGotchi.DataAccess.Models;
using System.Text.Json.Serialization;

namespace InnoGotchi.BusinessLogic.Dto
{
    public class BodyPartDto : DtoBase
    {
        public string? Image { get; set; }
        public string? Color { get; set; }
        public int BodyPartTypeId { get; set; }
        public BodyPartTypeDto? BodyPartType { get; set; }
        [JsonIgnore]
        public ICollection<PetDto>? Pets { get; set; } = new List<PetDto>();
    }
}
