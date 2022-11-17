using InnoGotchi.API.Responses;
using InnoGotchi.API.Settings;
using InnoGotchi.BusinessLogic.Components;
using InnoGotchi.BusinessLogic.Dto;
using InnoGotchi.BusinessLogic.Exceptions;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.DataAccess.Components;
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
        [Authorize]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddPet([FromBody] PetDto pet)
        {
            try
            {
                int? response = await _petService.AddNewPetAsync(pet);
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
        public async Task<IActionResult> UpdatePet([FromBody] PetDto petToUpdate)
        {
            try
            {
                var response = await _petService.UpdatePetAsync(petToUpdate);
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

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePet([FromRoute] int id)
        {
            try
            {
                var response = await _petService.RemovePetAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorResponse(ex.Message));
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(PetDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPetById([FromRoute] int id)
        {
            try
            {
                var response = await _petService.GetPetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorResponse(ex.Message));
            }
        }

        [HttpGet("farm/{id}")]
        [Authorize]
        [ProducesResponseType(typeof(List<PetDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPetByFarmId([FromRoute] int id)
        {
            try
            {
                var response = await _petService.GetPetsByFarmIdAsync(id);
                return Ok(response.ToList());
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorResponse(ex.Message));
            }
        }



        [HttpGet("page/{page}")]
        [Authorize]
        [ProducesResponseType(typeof(Page<PetDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPets([FromRoute] int page, 
            [FromServices] IOptions<PageSizeSettings> pageSizeSettings)
        {
            try
            {
                var response = await _petService.GetPetsAsyncAsPage(page, pageSizeSettings.Value.PageSize);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse(ex.Message));
            }
        }
    }
}
