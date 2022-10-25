using InnoGotchi.API.AuthOptions;
using InnoGotchi.API.Responses;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace InnoGotchi.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("innogotchi/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accService;

        public AccountController(IAccountService accService)
        {
            _accService = accService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Token(string username, string password)
        {
            var identity = await GetIdentity(username, password);
            if (identity == null)
                return BadRequest(new { errorText = "Invalid username or password." });

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                    issuer: AuthApiOptions.ISSUER,
                    audience: AuthApiOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthApiOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthApiOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Ok(new TockenResponse { AccessToken = encodedJwt, LoginInformation = identity.Name});
        }

        private async Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            var user = await _accService.GetUser(username, password);
            var role = await _accService.GetRole(user);
            if (user != null && role != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, role.Name)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }
    }
}
