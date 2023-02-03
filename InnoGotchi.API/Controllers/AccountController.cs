﻿using InnoGotchi.API.Responses;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.Components.DtoModels;
using InnoGotchi.Components.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchi.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("innogotchi/account")]  
    public class AccountController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public AccountController(IIdentityService identityService)
        {
            _identityService = identityService;
        }


        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthenticateResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SignIn([FromBody] AuthenticateRequestDto model)
        {
            var response = await _identityService.AuthenticateAsync(model);
            return CreatedAtAction(nameof(SignIn), response);
        }


        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthenticateResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SignUp([FromBody] IdentityUserDto model)
        {
            var response = await _identityService.RegisterAsync(model);
            return CreatedAtAction(nameof(SignUp), response);
        }
        

        [HttpGet("{username}")]
        [Authorize]
        [ProducesResponseType(typeof(AuthenticateResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserData([FromRoute] string username)
        {
            var response = await _identityService.GetReadonlyUserData(username);
            return Ok(response);
        }


        [HttpPut("change-password")]
        [Authorize]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdatePassword([FromBody] IdentityUserDto model)
        {
            var response = await _identityService.UpdateUserAsync(model, UpdateType.password);
            return Ok(response);
        }


        [HttpPut("change-user-info")]
        [Authorize]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateUserData([FromBody] IdentityUserDto model)
        {
            var response = await _identityService.UpdateUserAsync(model, UpdateType.user);
            return Ok(response);
        }

    }
}
