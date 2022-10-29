using InnoGotchi.API.Responses;
using InnoGotchi.BusinessLogic.AuthModels;
using InnoGotchi.BusinessLogic.Dto;
using InnoGotchi.BusinessLogic.Exceptions;
using InnoGotchi.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchi.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("innogotchi/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public AccountController(IIdentityService identityService)
        {
            _identityService = identityService;
        }


        [HttpPost("auth")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthenticateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> SignIn([FromBody] AuthenticateRequest model)
        {
            var response = await _identityService.AuthenticateAsync(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect!" });

            return Ok(response);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthenticateResponse), StatusCodes.Status200OK)]
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



        [HttpPut]
        [Authorize]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePassword([FromBody] IdentityUserDto model)
        {
            try
            {
                var response = await _identityService.UpdateUserAsync(model);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse(ex.Message));
            }
        }


    }
}
