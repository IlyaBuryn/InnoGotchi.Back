using System.Text.Json.Serialization;

namespace InnoGotchi.Components.DtoModels
{
    public class BodyPartDto : DtoBase
    {
        public string? Image { get; set; }
        public string? Color { get; set; }
        public int BodyPartTypeId { get; set; }
        public BodyPartTypeDto? BodyPartType { get; set; }
        [JsonIgnore]
        public List<PetDto>? Pets { get; set; } = new List<PetDto>();
    }
}
