using InnoGotchi.API.Responses;
using InnoGotchi.API.Settings;
using InnoGotchi.BusinessLogic.Dto;
using InnoGotchi.BusinessLogic.Exceptions;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.BusinessLogic.Services;
using InnoGotchi.DataAccess.Components;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

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
            try
            {
                int? response = await _farmService.CreateFarmAsync(farm);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteFarm([FromRoute] int id)
        {
            try
            {
                var response = await _farmService.DeleteFarmAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorResponse(ex.Message));
            }
        }

        [HttpPut]
        [AllowAnonymous]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateFarm([FromBody] FarmDto farmToUpdate)
        {
            try
            {
                var response = await _farmService.UpdateFarmAsync(farmToUpdate);
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

        [HttpGet("farm/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PetDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFarmById([FromRoute] int farmId)
        {
            try
            {
                var response = await _farmService.GetFarmByIdAsync(farmId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorResponse(ex.Message));
            }
        }

        [HttpGet("user/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PetDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFarmByUserId([FromRoute] int userId)
        {
            try
            {
                var response = await _farmService.GetFarmByUserIdAsync(userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorResponse(ex.Message));
            }
        }
    }
}
