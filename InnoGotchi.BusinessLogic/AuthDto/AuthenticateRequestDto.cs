using System.ComponentModel.DataAnnotations;

namespace InnoGotchi.BusinessLogic.AuthDto
{
    public class AuthenticateRequestDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string Role { get; set; } = "User";
    }
}
