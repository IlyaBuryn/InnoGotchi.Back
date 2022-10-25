namespace InnoGotchi.DataAccess.Models
{
    public class Collaborator : EntityBase
    {
        public Farm? Farm { get; set; }
        public IdentityUser User { get; set; }
    }
}
