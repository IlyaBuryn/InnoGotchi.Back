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
        private readonly IBodyPartService _bodyPartService;

        public BodyPartController(
            IBodyPartService bodyPartService)
        {
            _bodyPartService = bodyPartService;
        }


        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddBodyPartAsync([FromBody] BodyPartDto bodyPart)
        {
            if (!ModelState.IsValid)
            {
                throw new DataValidationException();
            }
            int? response = await _bodyPartService.AddNewBodyPartAsync(bodyPart);
            return CreatedAtAction(nameof(AddBodyPartAsync), response);
        }


        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBodyPartAsync([FromBody] BodyPartDto bodyPart)
        {
            if (!ModelState.IsValid)
            {
                throw new DataValidationException();
            }
            var response = await _bodyPartService.UpdateBodyPartAsync(bodyPart);
            return Ok(response);
        }


        [HttpDelete("delete/{bpId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBodyPartAsync([FromRoute] int bodyPartId)
        {
            var response = await _bodyPartService.RemoveBodyPartAsync(bodyPartId);
            return Ok(response);
        }


        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<BodyPartDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBodyPartsAsync()
        {

            var response = await _bodyPartService.GetBodyPartsAsync();
            return Ok(response);
        }


        [HttpGet("pet/{petId}")]
        [Authorize]
        [ProducesResponseType(typeof(List<BodyPartDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBodyPartsByPetAsync([FromRoute] int petId)
        {
            var response = await _bodyPartService.GetBodyPartsByPetIdAsync(petId);
            return Ok(response);
        }


        [HttpGet("type/{typeId}")]
        [Authorize]
        [ProducesResponseType(typeof(List<BodyPartDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBodyPartsByBodyPartTypeAsync([FromRoute] int bodyPartTypeId)
        {
            var response = await _bodyPartService.GetBodyPartsByTypeIdAsync(bodyPartTypeId);
            return Ok(response);
        }


        [HttpGet("{bpId}")]
        [Authorize]
        [ProducesResponseType(typeof(BodyPartDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBodyPartsByIdAsync([FromRoute] int bodyPartId)
        {
            var response = await _bodyPartService.GetBodyPartByIdAsync(bodyPartId);
            return Ok(response);
        }


        [HttpPost("body-part-type/create")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddBodyPartTypeAsync([FromBody] BodyPartTypeDto bodyPartType)
        {
            int? response = await _bodyPartService.CreateBodyPartTypeAsync(bodyPartType);
            return Ok(response);
        }


        [HttpDelete("body-part-type/delete/{typeId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBodyPartTypeAsync([FromRoute] int bodyPartTypeId)
        {
            var response = await _bodyPartService.DeleteBodyPartTypeAsync(bodyPartTypeId);
            return Ok(response);
        }
    }
}
