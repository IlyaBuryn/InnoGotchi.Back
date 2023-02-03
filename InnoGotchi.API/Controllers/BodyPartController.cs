﻿using InnoGotchi.API.Responses;
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

        public BodyPartController(IBodyPartService bpService)
        {
            _bpService = bpService;
        }


        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddBodyPart([FromBody] BodyPartDto bodyPart)
        {
            int? response = await _bpService.AddNewBodyPartAsync(bodyPart);
            return CreatedAtAction(nameof(AddBodyPart), response);
        }


        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBodyPart([FromBody] BodyPartDto bodyPart)
        {
            var response = await _bpService.UpdateBodyPartAsync(bodyPart);
            return Ok(response);
        }


        [HttpDelete("delete/{bpId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBodyPart([FromRoute] int bpId)
        {
            var response = await _bpService.RemoveBodyPartAsync(bpId);
            return Ok(response);
        }


        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<BodyPartDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBodyParts()
        {

            var response = await _bpService.GetBodyPartsAsync();
            return Ok(response);
        }


        [HttpGet("pet/{petId}")]
        [Authorize]
        [ProducesResponseType(typeof(List<BodyPartDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBodyPartsByPet([FromRoute] int petId)
        {
            var response = await _bpService.GetBodyPartsByPetIdAsync(petId);
            return Ok(response);
        }


        [HttpGet("type/{typeId}")]
        [Authorize]
        [ProducesResponseType(typeof(List<BodyPartDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBodyPartsByBodyPartType([FromRoute] int typeId)
        {
            var response = await _bpService.GetBodyPartsByTypeIdAsync(typeId);
            return Ok(response);
        }


        [HttpGet("{bpId}")]
        [Authorize]
        [ProducesResponseType(typeof(BodyPartDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBodyPartsById([FromRoute] int bpId)
        {
            var response = await _bpService.GetBodyPartByIdAsync(bpId);
            return Ok(response);
        }


        [HttpPost("body-part-type/create")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddBodyPartType([FromBody] BodyPartTypeDto bpType)
        {
            int? response = await _bpService.CreateBodyPartTypeAsync(bpType);
            return Ok(response);
        }


        [HttpDelete("body-part-type/delete/{typeId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBodyPartType([FromRoute] int typeId)
        {
            var response = await _bpService.DeleteBodyPartTypeAsync(typeId);
            return Ok(response);
        }
    }
}
