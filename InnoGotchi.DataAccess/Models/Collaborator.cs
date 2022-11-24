namespace InnoGotchi.DataAccess.Models
{
    public class Collaborator : EntityBase
    {
        public int FarmId { get; set; }
        public int IdentityUserId { get; set; }

        public virtual Farm Farm { get; set; }
        public virtual IdentityUser User { get; set; }
    }
}
