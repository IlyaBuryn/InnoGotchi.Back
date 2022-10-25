using InnoGotchi.BusinessLogic.Dto;

namespace InnoGotchi.BusinessLogic.Interfaces
{
    public interface IAccountService
    {
        public Task<int?> CreateUser(IdentityUserDto user);
        public Task UpdateUser(IdentityUserDto userToUpdate);
        public Task<IdentityUserDto?> GetUser(string username, string password);
        public Task<IdentityRoleDto?> GetRole(IdentityUserDto user);
    }
}
