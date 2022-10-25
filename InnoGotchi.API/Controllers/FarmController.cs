using InnoGotchi.API.Responses;
using InnoGotchi.BusinessLogic.Dto;
using InnoGotchi.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchi.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("innogotchi/[controller]")]
    public class FarmController : ControllerBase
    {
        private readonly IFarmService _farmService;

        public FarmController(IFarmService farmService)
        {
            _farmService = farmService;
        }


        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateFarm([FromBody] FarmDto farm)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.
                    Where(v => v is not null && v.Errors.Count > 0).
                SelectMany(v => v.Errors).Select(err => err.ErrorMessage);

                return BadRequest(new ErrorResponse { Errors = errors });
            }

            int? createdFarmId = await _farmService.CreateNewFarmAsync(farm);
            return Ok(createdFarmId);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteFarm([FromRoute] int id)
        {
            await _farmService.DeleteFarmAsync(id);

            return Ok();
        }


        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PetDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFarmById([FromRoute] int id)
        {
            var pet = await _farmService.GetFarmByIdAsync(id);

            if (pet is null)
                return NotFound(new ErrorResponse { Errors = new List<string> { "Farm with the specified id not found!" } });

            return Ok(pet);
        }
    }
}
