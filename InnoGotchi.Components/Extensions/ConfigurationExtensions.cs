using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace InnoGotchi.Components.Extensions
{
    public static class ConfigurationExtensions
    {
        public static TokenValidationParameters GetJwtOptions(this IConfiguration configuration)
        {
            var jwtKey = configuration["Jwt:Key"];
            if (jwtKey == null)
            {
                throw new ArgumentNullException("Jwt:Key value is missing from configuration");
            }
            var key = Encoding.UTF8.GetBytes(jwtKey);

            return new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
            };
        }
    }
}
