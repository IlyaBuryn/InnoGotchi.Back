using InnoGotchi.API.Responses;
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
        [ProducesResponseType(typeof(AuthenticateResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SignIn([FromBody] AuthenticateRequestDto model)
        {
            try
            {
                var response = await _identityService.AuthenticateAsync(model);
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest(new ErrorResponse("Username or password is incorrect!"));
            }
        }

        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthenticateResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SignUp([FromBody] IdentityUserDto model)
        {
            try
            {
                var response = await _identityService.RegisterAsync(model);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse(ex.Message));
            }
        }

        [HttpGet("{username}")]
        [Authorize]
        [ProducesResponseType(typeof(AuthenticateResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserData([FromRoute] string username)
        {
            try
            {
                var response = await _identityService.GetReadonlyUserData(username);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse(ex.Message));
            }
        }



        [HttpPut("change-password")]
        [Authorize]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdatePassword([FromBody] IdentityUserDto model)
        {
            try
            {
                var response = await _identityService.UpdateUserAsync(model, UpdateType.password);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse(ex.Message));
            }
        }

        [HttpPut("change-user-info")]
        [Authorize]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateUserData([FromBody] IdentityUserDto model)
        {
            try
            {
                var response = await _identityService.UpdateUserAsync(model, UpdateType.user);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse(ex.Message));
            }
        }

    }
}
