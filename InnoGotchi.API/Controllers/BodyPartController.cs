using FluentValidation;
using InnoGotchi.API.Responses;
using InnoGotchi.BusinessLogic.Exceptions;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.Components.DtoModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchi.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("innogotchi/body-parts")]
    public class BodyPartController : ControllerBase
    {
        private readonly IBodyPartService _bpService;
        private readonly IValidator<BodyPartDto> _bpValidator;

        public BodyPartController(
            IBodyPartService bpService, 
            IValidator<BodyPartDto> bpValidator)
        {
            _bpService = bpService;
            _bpValidator = bpValidator;
        }


        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddBodyPartAsync([FromBody] BodyPartDto bodyPart)
        {
            var validationResult = await _bpValidator.ValidateAsync(bodyPart);
            if (!validationResult.IsValid)
            {
                throw new DataValidationException();
            }
            int? response = await _bpService.AddNewBodyPartAsync(bodyPart);
            return CreatedAtAction(nameof(AddBodyPartAsync), response);
        }


        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBodyPartAsync([FromBody] BodyPartDto bodyPart)
        {
            var validationResult = await _bpValidator.ValidateAsync(bodyPart);
            if (!validationResult.IsValid)
            {
                throw new DataValidationException();
            }
            var response = await _bpService.UpdateBodyPartAsync(bodyPart);
            return Ok(response);
        }


        [HttpDelete("delete/{bpId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBodyPartAsync([FromRoute] int bpId)
        {
            var response = await _bpService.RemoveBodyPartAsync(bpId);
            return Ok(response);
        }


        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<BodyPartDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public IActionResult GetBodyParts()
        {

            var response = _bpService.GetBodyParts();
            return Ok(response);
        }


        [HttpGet("pet/{petId}")]
        [Authorize]
        [ProducesResponseType(typeof(List<BodyPartDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public IActionResult GetBodyPartsByPet([FromRoute] int petId)
        {
            var response = _bpService.GetBodyPartsByPetId(petId);
            return Ok(response);
        }


        [HttpGet("type/{typeId}")]
        [Authorize]
        [ProducesResponseType(typeof(List<BodyPartDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public IActionResult GetBodyPartsByBodyPartType([FromRoute] int typeId)
        {
            var response = _bpService.GetBodyPartsByTypeId(typeId);
            return Ok(response);
        }


        [HttpGet("{bpId}")]
        [Authorize]
        [ProducesResponseType(typeof(BodyPartDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBodyPartsByIdAsync([FromRoute] int bpId)
        {
            var response = await _bpService.GetBodyPartByIdAsync(bpId);
            return Ok(response);
        }


        [HttpPost("body-part-type/create")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddBodyPartTypeAsync([FromBody] BodyPartTypeDto bpType)
        {
            int? response = await _bpService.CreateBodyPartTypeAsync(bpType);
            return Ok(response);
        }


        [HttpDelete("body-part-type/delete/{typeId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBodyPartTypeAsync([FromRoute] int typeId)
        {
            var response = await _bpService.DeleteBodyPartTypeAsync(typeId);
            return Ok(response);
        }
    }
}
