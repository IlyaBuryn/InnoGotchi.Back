namespace InnoGotchi.Components.DtoModels
{
    public class FarmDto : DtoBase
    {
        public string Name { get; set; }
        public int IdentityUserId { get; set; }

        public virtual List<PetDto>? Pets { get; set; }
    }
}
