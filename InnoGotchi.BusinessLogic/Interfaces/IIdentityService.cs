using InnoGotchi.BusinessLogic.AuthModels;
using InnoGotchi.BusinessLogic.Dto;

namespace InnoGotchi.BusinessLogic.Interfaces
{
    public interface IIdentityService
    {
        public Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model);
        public Task<AuthenticateResponse> RegisterAsync(IdentityUserDto userToRegister);
        public Task<int?> CreateRoleAsync(IdentityRoleDto roleCreate);
        public Task<bool> UpdateUserAsync(IdentityUserDto userToUpdate);
    }
}
