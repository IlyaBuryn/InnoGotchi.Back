using FluentValidation;
using InnoGotchi.API.Responses;
using InnoGotchi.BusinessLogic.Exceptions;
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
        private readonly IValidator<IdentityUserDto> _userValidator;
        private readonly IValidator<IdentityRoleDto> _roleValidator;

        public AccountController(
            IIdentityService identityService,
            IValidator<IdentityRoleDto> roleValidator,
            IValidator<IdentityUserDto> userValidator)
        {
            _identityService = identityService;
            _userValidator = userValidator;
            _roleValidator = roleValidator;
        }


        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthenticateResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public IActionResult SignIn([FromBody] AuthenticateRequestDto model)
        {
            var response = _identityService.Authenticate(model);
            return CreatedAtAction(nameof(SignIn), response);
        }


        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthenticateResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SignUpAsync([FromBody] IdentityUserDto model)
        {
            var validationResult = await _userValidator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                throw new DataValidationException();
            }
            var response = await _identityService.RegisterAsync(model);
            return CreatedAtAction(nameof(SignUpAsync), response);
        }
        

        [HttpGet("{username}")]
        [Authorize]
        [ProducesResponseType(typeof(AuthenticateResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserDataAsync([FromRoute] string username)
        {
            var response = await _identityService.GetReadonlyUserData(username);
            return Ok(response);
        }


        [HttpPut("change-password")]
        [Authorize]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdatePasswordAsync([FromBody] IdentityUserDto model)
        {
            var validationResult = await _userValidator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                throw new DataValidationException();
            }
            var response = await _identityService.UpdateUserAsync(model, UpdateType.password);
            return Ok(response);
        }


        [HttpPut("change-user-info")]
        [Authorize]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateUserDataAsync([FromBody] IdentityUserDto model)
        {
            var response = await _identityService.UpdateUserAsync(model, UpdateType.user);
            return Ok(response);
        }

    }
}
