using InnoGotchi.API.Responses;
using InnoGotchi.BusinessLogic.Dto;
using InnoGotchi.BusinessLogic.Exceptions;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchi.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("innogotchi/[controller]")]
    public class BodyPartController : ControllerBase
    {
        private readonly IBodyPartService _bpService;

        public BodyPartController(IBodyPartService bpService)
        {
            _bpService = bpService;
        }

        [HttpPost("bodypart")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddBodyPart([FromBody] BodyPartDto bodyPart)
        {
            try
            {
                int? response = await _bpService.AddNewBodyPartAsync(bodyPart);
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

        [HttpPut("bodypart")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBodyPart([FromBody] BodyPartDto bodyPart)
        {
            try
            {
                var response = await _bpService.UpdateBodyPartAsync(bodyPart);
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

        [HttpDelete("bodypart/{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBodyPart([FromRoute] int id)
        {
            try
            {
                var response = await _bpService.RemoveBodyPartAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorResponse(ex.Message));
            }
        }

        [HttpGet("bodypart")]
        [Authorize]
        [ProducesResponseType(typeof(List<BodyPartDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBodyParts()
        {
            try
            {
                var response = await _bpService.GetBodyPartsAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorResponse(ex.Message));
            }
        }

        [HttpGet("bodypart/pet/{id}")]
        [Authorize]
        [ProducesResponseType(typeof(List<BodyPartDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBodyPartsByPet([FromRoute] int id)
        {
            try
            {
                var response = await _bpService.GetBodyPartsByPetIdAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorResponse(ex.Message));
            }
        }

        [HttpGet("bodypart/type/{id}")]
        [Authorize]
        [ProducesResponseType(typeof(List<BodyPartDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBodyPartsByBodyPartType([FromRoute] int id)
        {
            try
            {
                var response = await _bpService.GetBodyPartsByTypeIdAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorResponse(ex.Message));
            }
        }

        [HttpGet("bodypart/{id}")]
        [Authorize]
        [ProducesResponseType(typeof(BodyPartDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBodyPartsById([FromRoute] int id)
        {
            try
            {
                var response = await _bpService.GetBodyPartByIdAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorResponse(ex.Message));
            }
        }

        [HttpPost("bptype")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddBodyPart([FromBody] BodyPartTypeDto bpType)
        {
            try
            {
                int? response = await _bpService.CreateBodyPartTypeAsync(bpType);
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

        [HttpDelete("bptype/{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBodyPartType([FromRoute] int id)
        {
            try
            {
                var response = await _bpService.DeleteBodyPartTypeAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorResponse(ex.Message));
            }
        }
    }
}
