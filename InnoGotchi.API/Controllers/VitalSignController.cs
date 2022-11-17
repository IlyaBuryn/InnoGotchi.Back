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
    [Route("innogotchi/[controller]")]
    public class VitalSignController : ControllerBase
    {
        private readonly IVitalSignService _vitalSignService;

        public VitalSignController(IVitalSignService vitalSignService)
        {
            _vitalSignService = vitalSignService;
        }

        [HttpPost]
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

        [HttpPut]
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

        [HttpGet("vitalsign/{id}")]
        [Authorize]
        [ProducesResponseType(typeof(VitalSignDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVitalSignById([FromRoute] int id)
        {
            try
            {
                var response = await _vitalSignService.GetVitalSignByIdAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorResponse(ex.Message));
            }
        }

        [HttpGet("pet/{id}")]
        [Authorize]
        [ProducesResponseType(typeof(VitalSignDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVitalSignByPetId([FromRoute] int id)
        {
            try
            {
                var response = await _vitalSignService.GetVitalSignByPetIdAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorResponse(ex.Message));
            }
        }
    }
}
