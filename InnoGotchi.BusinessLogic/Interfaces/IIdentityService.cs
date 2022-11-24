using InnoGotchi.Components.DtoModels;
using InnoGotchi.Components.Enums;

namespace InnoGotchi.BusinessLogic.Interfaces
{
    public interface IIdentityService
    {
        public Task<AuthenticateResponseDto> AuthenticateAsync(AuthenticateRequestDto model);
        public Task<AuthenticateResponseDto> RegisterAsync(IdentityUserDto userToRegister);
        public Task<int?> CreateRoleAsync(IdentityRoleDto roleCreate);
        public Task<bool> UpdateUserAsync(IdentityUserDto userToUpdate, UpdateType updateType);
        public Task<AuthenticateResponseDto> GetReadonlyUserData(string username);
    }
}
