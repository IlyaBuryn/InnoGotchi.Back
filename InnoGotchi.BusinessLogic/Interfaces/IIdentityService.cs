﻿using InnoGotchi.Components.DtoModels;
using InnoGotchi.Components.Enums;

namespace InnoGotchi.BusinessLogic.Interfaces
{
    public interface IIdentityService
    {
        AuthenticateResponseDto Authenticate(AuthenticateRequestDto model);
        Task<AuthenticateResponseDto> RegisterAsync(IdentityUserDto userToRegister);
        Task<bool> UpdateUserAsync(IdentityUserDto userToUpdate, UpdateType updateType);
        Task<AuthenticateResponseDto> GetReadonlyUserData(string username);
    }
}
