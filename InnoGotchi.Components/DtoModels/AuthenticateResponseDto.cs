namespace InnoGotchi.Components.DtoModels
{
    public class AuthenticateResponseDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Image { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }

        public AuthenticateResponseDto() { }
    }
}
