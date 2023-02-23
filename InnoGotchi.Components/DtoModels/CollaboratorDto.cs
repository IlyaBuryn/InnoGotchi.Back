namespace InnoGotchi.Components.DtoModels
{
    public class CollaboratorDto : DtoBase
    {
        public int FarmId { get; set; }
        public int IdentityUserId { get; set; }

        public FarmDto? Farm { get; set; }
    }
}
