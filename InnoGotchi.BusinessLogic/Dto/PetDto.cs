namespace InnoGotchi.BusinessLogic.Dto
{
    public class PetDto : DtoBase
    {
        public string Name { get; set; }
        public int FarmId { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
