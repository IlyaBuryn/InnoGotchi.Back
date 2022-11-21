using InnoGotchi.API.Responses;
using InnoGotchi.BusinessLogic.Dto;
using InnoGotchi.BusinessLogic.Exceptions;
using InnoGotchi.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchi.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("innogotchi/vital-sign")]
    public class VitalSignController : ControllerBase
    {
        private readonly IVitalSignService _vitalSignService;

        public VitalSignController(IVitalSignService vitalSignService)
        {
            _vitalSignService = vitalSignService;
        }

        [HttpPost("create")]
        [Authorize]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddVitalSign([FromBody] VitalSignDto vitalSign)
        {
            try
            {
                int? response = await _vitalSignService.CreateVitalSignAsync(vitalSign);
                return Ok(response);
            }
            catch (DataValidationException ex)
            {
                return BadRequest(new ErrorResponse(ex.Message));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorResponse(ex.Message));
            }
        }

        [HttpPut("update")]
        [Authorize]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateVitalSign([FromBody] VitalSignDto vitalSign)
        {
            try
            {
                var response = await _vitalSignService.UpdateVitalSignAsync(vitalSign);
                return Ok(response);
            }
            catch (DataValidationException ex)
            {
                return BadRequest(new ErrorResponse(ex.Message));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorResponse(ex.Message));
            }
        }

        [HttpGet("{vsId}")]
        [Authorize]
        [ProducesResponseType(typeof(VitalSignDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVitalSignById([FromRoute] int vsId)
        {
            try
            {
                var response = await _vitalSignService.GetVitalSignByIdAsync(vsId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorResponse(ex.Message));
            }
        }

        [HttpGet("pet/{petId}")]
        [Authorize]
        [ProducesResponseType(typeof(VitalSignDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVitalSignByPetId([FromRoute] int petId)
        {
            try
            {
                var response = await _vitalSignService.GetVitalSignByPetIdAsync(petId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorResponse(ex.Message));
            }
        }
    }
}
