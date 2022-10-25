using InnoGotchi.API.Responses;
using InnoGotchi.API.Settings;
using InnoGotchi.BusinessLogic.Components;
using InnoGotchi.BusinessLogic.Dto;
using InnoGotchi.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace InnoGotchi.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("innogotchi/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetsController(IPetService petService)
        {
            _petService = petService;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddPet([FromBody] PetDto pet)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.
                    Where(v => v is not null && v.Errors.Count > 0).
                    SelectMany(v => v.Errors).Select(err => err.ErrorMessage);

                return BadRequest(new ErrorResponse { Errors = errors });
            }

            int? createdPetId = await _petService.AddNewPetAsync(pet);
            return Ok(createdPetId);
        }

        [HttpPut]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePetParts([FromBody] PetDto petToUpdate)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.
                    Where(v => v is not null && v.Errors.Count > 0).
                    SelectMany(v => v.Errors).Select(err => err.ErrorMessage);

                return BadRequest(new ErrorResponse { Errors = errors });
            }

            await _petService.UpdatePetAsync(petToUpdate);

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePet([FromRoute] int id)
        {
            await _petService.RemovePetAsync(id);

            return Ok();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PetDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPetById([FromRoute] int id)
        {
            var pet = await _petService.GetPetByIdAsync(id);

            if (pet is null)
                return NotFound(new ErrorResponse { Errors = new List<string> { "Pet with the specified id not found!" } });

            return Ok(pet);
        }


        [HttpGet("page/{page}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Page<PetDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPets([FromRoute] int page, 
            [FromServices] IOptions<PageSizeSettings> pageSizeSettings)
        {
            return Ok(await _petService.GetPetsAsync(page, pageSizeSettings.Value.PageSize));
        }
    }
}
