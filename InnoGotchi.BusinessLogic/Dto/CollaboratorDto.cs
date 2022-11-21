namespace InnoGotchi.BusinessLogic.Dto
{
    public class CollaboratorDto : DtoBase
    {
        public int FarmId { get; set; }
        public int IdentityUserId { get; set; }

        public virtual FarmDto? Farm { get; set; }
    }
}
