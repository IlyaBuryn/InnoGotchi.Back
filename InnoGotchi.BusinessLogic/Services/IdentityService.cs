using AutoMapper;
using InnoGotchi.BusinessLogic.Exceptions;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.Components.DtoModels;
using InnoGotchi.Components.Enums;
using InnoGotchi.Components.Settings;
using InnoGotchi.DataAccess.Interfaces;
using InnoGotchi.DataAccess.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace InnoGotchi.BusinessLogic.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IRepository<IdentityUser> _userRep;
        private readonly IRepository<IdentityRole> _roleRep;
        private readonly IMapper _mapper;
        private readonly JwtOptions _jwtOptions;

        public IdentityService(IRepository<IdentityUser> userRep,
            IRepository<IdentityRole> roleRep,
            IMapper mapper,
            IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
            _userRep = userRep;
            _roleRep = roleRep;
            _mapper = mapper;
        }

        public async Task<AuthenticateResponseDto> AuthenticateAsync(AuthenticateRequestDto model)
        {
            var user = await _userRep.GetOneAsync(
                expression: x => x.Username == model.Username && x.Password == model.Password, 
                includeProperties: u => u.Role);

            if (user == null)
            {
                throw new AuthenticateException();
            }

            if (user.Role == null)
            {
                throw new AuthenticateException("User not having a valid role.");
            }

            var token = CreateJwtToken(user);

            return new AuthenticateResponseDto()
            {
                Id = user.Id,
                Username = user.Username,
                Name = user.Name,
                Surname = user.Surname,
                Role = user.Role.Name,
                Image = user.Image,
                Token = token,
            };
        }

        public async Task<AuthenticateResponseDto> RegisterAsync(IdentityUserDto userToRegister)
        {
            var user = _mapper.Map<IdentityUser>(userToRegister);

            if (user.IdentityRoleId == default)
            {
                var role = await _roleRep.GetOneAsync(x => x.Name == DefaultSettings.DefaultRole);
                if (role == null)
                {
                    throw new NotFoundException("Server cannot find the role!");
                }

                user.IdentityRoleId = role.Id;
                user.Role = role;
            }

            var userToAdd = await _userRep.GetOneAsync(x => x.Username == userToRegister.Username);
            if (userToAdd != null)
            {
                throw new DataValidationException("This user already exist!");
            }

            var addedUser = await _userRep.AddAsync(user);

            var response = await AuthenticateAsync(new AuthenticateRequestDto
            {
                Username = user.Username,
                Password = user.Password,
                Name = user.Name,
                Surname = user.Surname,
            });

            return response;
        }

        public async Task<AuthenticateResponseDto> GetReadonlyUserData(string username)
        {
            var user = await _userRep.GetOneAsync(x => x.Username == username);
            if (user == null)
            {
                throw new NotFoundException(nameof(user));
            }

            return new AuthenticateResponseDto()
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Image = user.Image,
                Username = user.Username,
            };
        }

        public async Task<bool> UpdateUserAsync(IdentityUserDto userToUpdate, UpdateType updateType)
        {
            var user = await _userRep.GetOneAsync(
                expression: x => x.Id == userToUpdate.Id && x.Username == userToUpdate.Username,
                includeProperties: x => x.Role);

            if (user == null)
            {
                throw new NotFoundException(nameof(user));
            }

            if (updateType == UpdateType.user)
            {
                user.Name = userToUpdate.Name;
                user.Surname = userToUpdate.Surname;
                user.Image = userToUpdate.Image;
            }

            if (updateType == UpdateType.password)
            {
                user.Password = userToUpdate.Password;
            }

            var mappedUser = _mapper.Map<IdentityUser>(user);
            return await _userRep.UpdateAsync(mappedUser);
        }

        private string CreateJwtToken(IdentityUser user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var jwtKey = _jwtOptions.Key;
            if (jwtKey == null)
            {
                throw new ArgumentNullException("Jwt:Key value is missing from configuration");
            }

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
