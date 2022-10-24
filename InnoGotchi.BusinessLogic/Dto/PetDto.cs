using InnoGotchi.DataAccess.Models;

namespace InnoGotchi.BusinessLogic.Dto
{
    public class PetDto : DtoBase
    {
        public string Name { get; set; }
        public int FarmId { get; set; }
        public int? BodyId { get; set; }
        public int? EyeId { get; set; }
        public int? NoseId { get; set; }
        public int? MouthId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
