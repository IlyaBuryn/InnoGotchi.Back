using AutoMapper;
using InnoGotchi.BusinessLogic.Dto;
using InnoGotchi.BusinessLogic.Exceptions;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.DataAccess.Interfaces;
using InnoGotchi.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<IdentityUser> _userRep;
        private readonly IRepository<IdentityRole> _roleRep;
        private readonly IMapper _mapper;

        public AccountService(IRepository<IdentityUser> userRep,
            IRepository<IdentityRole> roleRep,
            IMapper mapper)
        {
            _userRep = userRep;
            _roleRep = roleRep;
            _mapper = mapper;
        }

        public async Task<int?> CreateUser(IdentityUserDto user)
        {
            var tmpUser = _userRep.GetAll()
                .Where(x => x.Username == user.Username)
                .FirstOrDefault();
            if (tmpUser != null)
                throw new DataValidationException("This user already exist!");

            var userRole = await _roleRep.GetByIdAsync(user.RoleId);
            if (userRole == null)
                throw new NotFoundException("This role does not exist!");

            var userId = await _userRep.AddAsync(_mapper.Map<IdentityUser>(user));
            if (userId == null)
                throw new NotFoundException("The user role could not be set because the user id was not obtained!");

            return userId;
        }

        public async Task<IdentityUserDto?> GetUser(string username, string password)
        {
            var user = _userRep.GetAll().Include(x => x.Role)
                .Where(x => x.Password == password && x.Username == username)
                .FirstOrDefault();
            if (user == null)
                throw new NotFoundException("This user does not exist!");

            return _mapper.Map<IdentityUserDto>(user);
        }

        public async Task<IdentityRoleDto?> GetRole(IdentityUserDto user)
        {
            var tmpUser = _userRep.GetAll().Include(x => x.Role)
                .Where(x => x.Password == user.Password && x.Username == user.Username)
                .FirstOrDefault();
            if (tmpUser == null)
                throw new NotFoundException("This user does not exist!");

            var role = await _roleRep.GetByIdAsync(user.RoleId);
            if (role == null)
                throw new NotFoundException("No links found!");

            return _mapper.Map<IdentityRoleDto>(role);
        }

        public async Task UpdateUser(IdentityUserDto userToUpdate)
        {
            var user = await _userRep.GetByIdAsync(userToUpdate.Id);
            if (user == null)
                throw new NotFoundException("This user does not exist!");
            if (user.Password != userToUpdate.Password)
                throw new DataValidationException("User update valiedation error!");

            await _userRep.UpdateAsync(_mapper.Map<IdentityUser>(userToUpdate));
        }
    }
}
