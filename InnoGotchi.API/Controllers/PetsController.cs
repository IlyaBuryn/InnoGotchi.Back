﻿using FluentValidation;
using InnoGotchi.API.Responses;
using InnoGotchi.BusinessLogic.Exceptions;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.Components.DtoModels;
using InnoGotchi.Components.Enums;
using InnoGotchi.Components.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace InnoGotchi.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("innogotchi/pet")]
    public class PetsController : ControllerBase
    {
        private readonly IPetService _petService;
        private readonly IValidator<PetDto> _petValidator;

        public PetsController(IPetService petService,
            IValidator<PetDto> petValidator)
        {
            _petService = petService;
            _petValidator = petValidator;
        }

        [HttpPost("create")]
        [Authorize]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddPetAsync([FromBody] PetDto pet)
        {
            var validationResult = await _petValidator.ValidateAsync(pet);
            if (!validationResult.IsValid)
            {
                throw new DataValidationException();
            }
            int? response = await _petService.AddNewPetAsync(pet);
            return CreatedAtAction(nameof(AddPetAsync), response);
        }


        [HttpPut("update")]
        [Authorize]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePetAsync([FromBody] PetDto petToUpdate)
        {
            var validationResult = await _petValidator.ValidateAsync(petToUpdate);
            if (!validationResult.IsValid)
            {
                throw new DataValidationException();
            }
            var response = await _petService.UpdatePetAsync(petToUpdate);
            return Ok(response);
        }


        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePetAsync([FromRoute] int id)
        {
            var response = await _petService.RemovePetAsync(id);
            return Ok(response);
        }


        [HttpGet("{petId}")]
        [Authorize]
        [ProducesResponseType(typeof(PetDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public IActionResult GetPetById([FromRoute] int petId)
        {
            var response = _petService.GetPetById(petId);
            return Ok(response);
        }


        [HttpGet("farm/{farmId}")]
        [Authorize]
        [ProducesResponseType(typeof(List<PetDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public IActionResult GetPetByFarmId([FromRoute] int farmId)
        {
            var response = _petService.GetPetsByFarmId(farmId);
            return Ok(response.ToList());
        }


        [HttpGet("page/{pageNumber}/{sortFilter}")]
        [Authorize]
        [ProducesResponseType(typeof(List<PetDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPetsAsync([FromRoute] int pageNumber, 
            [FromServices] IOptions<PageSizeSettings> pageSizeSettings,[FromRoute] SortFilter sortFilter)
        {
            var response = await _petService.GetPetsAsyncAsPageAsync(pageNumber, pageSizeSettings.Value.PageSize, sortFilter);
            return Ok(response);
        }


        [HttpGet("all-count")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public IActionResult GetPetsCount()
        {
            var response = _petService.GetAllPetsCount();
            return Ok(response);
        }
    }
}
