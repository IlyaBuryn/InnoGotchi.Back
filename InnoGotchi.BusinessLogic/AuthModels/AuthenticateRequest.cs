using System.ComponentModel.DataAnnotations;

namespace InnoGotchi.BusinessLogic.AuthModels
{
    public class AuthenticateRequest
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
