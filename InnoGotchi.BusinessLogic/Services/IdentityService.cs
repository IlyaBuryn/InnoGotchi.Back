using AutoMapper;
using FluentValidation;
using InnoGotchi.BusinessLogic.AuthModels;
using InnoGotchi.BusinessLogic.Components;
using InnoGotchi.BusinessLogic.Dto;
using InnoGotchi.BusinessLogic.Exceptions;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.DataAccess.Interfaces;
using InnoGotchi.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace InnoGotchi.BusinessLogic.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IRepository<IdentityUser> _userRep;
        private readonly IRepository<IdentityRole> _roleRep;
        private readonly IValidator<IdentityUser> _userValidator;
        private readonly IValidator<IdentityRole> _roleValidator;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public IdentityService(IRepository<IdentityUser> userRep,
            IRepository<IdentityRole> roleRep,
            IValidator<IdentityRole> roleValidator,
            IValidator<IdentityUser> userValidator,
            IConfiguration configuration,
            IMapper mapper)
        {
            _userValidator = userValidator;
            _roleValidator = roleValidator;
            _configuration = configuration;
            _userRep = userRep;
            _roleRep = roleRep;
            _mapper = mapper;
        }

        public async Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model)
        {
            var user = (await _userRep.GetAllAsync(x => x.Username == model.Username
                && x.Password == model.Password)).Include(u => u.Role).FirstOrDefault();
            if (user == null)
                throw new AuthenticateException();

            if (user.Role == null)
                throw new NotFoundException(nameof(user.Role));

            var token = CreateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public async Task<AuthenticateResponse> RegisterAsync(IdentityUserDto userToRegister)
        {
            var validationResult = await _userValidator.ValidateAsync(_mapper.Map<IdentityUser>(userToRegister));
            if (!validationResult.IsValid)
                throw new DataValidationException();

            var user = _mapper.Map<IdentityUser>(userToRegister);

            if (user.IdentityRoleId == default)
            {
                var role = await _roleRep.GetOneAsync(x => x.Name == DefaultSettings.DefaultRole);
                if (role == null)
                    throw new NotFoundException("Server cannot find the role!");

                user.IdentityRoleId = role.Id;
                user.Role = role;
            }

            var userToAdd = await _userRep.GetOneAsync(x => x.Username == userToRegister.Username);
            if (userToAdd != null)
                throw new DataValidationException("This user already exist!");

            var addedUser = await _userRep.AddAsync(user);

            var response = await AuthenticateAsync(new AuthenticateRequest
            {
                Username = user.Username,
                Password = user.Password,
                Name = user.Name,
                Surname = user.Surname,
            });
            return response;
        }

        public async Task<int?> CreateRoleAsync(IdentityRoleDto roleToCreate)
        {
            var validationResult = await _roleValidator.ValidateAsync(_mapper.Map<IdentityRole>(roleToCreate));
            if (!validationResult.IsValid)
                throw new DataValidationException();

            var role = await _roleRep.GetOneAsync(x => x.Name == roleToCreate.Name);
            if (role != null)
                throw new DataValidationException("This user already exist!");

            return await _roleRep.AddAsync(_mapper.Map<IdentityRole>(roleToCreate));
        }

        public async Task<bool> UpdateUserAsync(IdentityUserDto userToUpdate)
        {
            var validationResult = await _userValidator.ValidateAsync(_mapper.Map<IdentityUser>(userToUpdate));

            if (!validationResult.IsValid)
                throw new DataValidationException();

            var user = await TryFindUser(userToUpdate);
            if (user == null)
                throw new NotFoundException(nameof(user));

            user.Password = userToUpdate.Password;
            user.Name = userToUpdate.Name;
            user.Surname = userToUpdate.Surname;
            user.Image = userToUpdate.Image;

            return await _userRep.UpdateAsync(_mapper.Map<IdentityUser>(user));
        }

        private string CreateJwtToken(IdentityUser user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("Jwt:Key").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private async Task<IdentityUser?> TryFindUser(IdentityUserDto userToFind)
        {
            return (await _userRep.GetAllAsync(x => x.Password == userToFind.Password
                && x.Username == userToFind.Username))
                .Include(x => x.Role)
                .FirstOrDefault();
        }
    }
}
