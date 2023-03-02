namespace InnoGotchi.Components.DtoModels
{
    public class FarmDto : DtoBase
    {
        public string Name { get; set; }
        public int IdentityUserId { get; set; }

        public List<PetDto>? Pets { get; set; }
    }
}
