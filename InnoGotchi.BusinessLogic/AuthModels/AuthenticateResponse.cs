using InnoGotchi.DataAccess.Models;

namespace InnoGotchi.BusinessLogic.AuthModels
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }

        public AuthenticateResponse(IdentityUser user, string token)
        {
            Id = user.Id;
            Username = user.Username;
            Name = user.Name;
            Surname = user.Surname;
            Role = user.Role.Name;
            Token = token;
        }
    }
}
