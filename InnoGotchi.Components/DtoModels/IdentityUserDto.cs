namespace InnoGotchi.Components.DtoModels
{
    public class IdentityUserDto : DtoBase
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Image { get; set; }
        public int IdentityRoleId { get; set; }
    }
}
