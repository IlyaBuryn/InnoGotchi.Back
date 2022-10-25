using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace InnoGotchi.API.AuthOptions
{
    public class AuthApiOptions
    {
        public const string ISSUER = "InnoGotchi.API";
        public const string AUDIENCE = "InnoGotchi.WEB";
        const string KEY = "8Q34VGBT3Q0431T0391949NXQ304RQC";
        public const int LIFETIME = 5;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
