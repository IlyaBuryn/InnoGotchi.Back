using InnoGotchi.API.Responses;
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

        public PetsController(IPetService petService)
        {
            _petService = petService;
        }

        [HttpPost("create")]
        [Authorize]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddPet([FromBody] PetDto pet)
        {
            int? response = await _petService.AddNewPetAsync(pet);
            return CreatedAtAction(nameof(AddPet), response);
        }


        [HttpPut("update")]
        [Authorize]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePet([FromBody] PetDto petToUpdate)
        {
            var response = await _petService.UpdatePetAsync(petToUpdate);
            return Ok(response);
        }


        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePet([FromRoute] int id)
        {
            var response = await _petService.RemovePetAsync(id);
            return Ok(response);
        }


        [HttpGet("{petId}")]
        [Authorize]
        [ProducesResponseType(typeof(PetDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPetById([FromRoute] int petId)
        {
            var response = await _petService.GetPetByIdAsync(petId);
            return Ok(response);
        }


        [HttpGet("farm/{farmId}")]
        [Authorize]
        [ProducesResponseType(typeof(List<PetDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPetByFarmId([FromRoute] int farmId)
        {
            var response = await _petService.GetPetsByFarmIdAsync(farmId);
            return Ok(response.ToList());
        }


        [HttpGet("page/{pageNumber}/{sortFilter}")]
        [Authorize]
        [ProducesResponseType(typeof(List<PetDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPets([FromRoute] int pageNumber, 
            [FromServices] IOptions<PageSizeSettings> pageSizeSettings,[FromRoute] SortFilter sortFilter)
        {
            var response = await _petService.GetPetsAsyncAsPage(pageNumber, pageSizeSettings.Value.PageSize, sortFilter);
            return Ok(response);
        }

        
        [HttpGet("all-count")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPetsCount()
        {
            var response = await _petService.GetAllPetsCount();
            return Ok(response);
        }
    }
}
