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
    [Route("innogotchi/farm")]
    public class FarmController : ControllerBase
    {
        private readonly IFarmService _farmService;
        private readonly IFeedService _feedService;

        public FarmController(IFarmService farmService, IFeedService feedService)
        {
            _farmService = farmService;
            _feedService = feedService;
        }


        [HttpPost("create")]
        [Authorize]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateFarm([FromBody] FarmDto farm)
        {
            try
            {
                int? response = await _farmService.CreateFarmAsync(farm);
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

        [HttpDelete("delete/{id}")]
        [Authorize]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
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

        [HttpPut("update")]
        [Authorize]
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

        [HttpGet("{farmId}")]
        [Authorize]
        [ProducesResponseType(typeof(FarmDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFarmById([FromRoute] int farmId)
        {
            try
            {
                var response = await _farmService.GetFarmByIdAsync(farmId);
                if (response != null && response.Id != 0)
                    await _feedService.RecalculatePetsNeeds(response.Id);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorResponse(ex.Message));
            }
        }

        [HttpGet("user/{userId}")]
        [Authorize]
        [ProducesResponseType(typeof(FarmDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFarmByUserId([FromRoute] int userId)
        {
            try
            {
                var response = await _farmService.GetFarmByUserIdAsync(userId);
                if (response != null && response.Id != 0)
                    await _feedService.RecalculatePetsNeeds(response.Id);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorResponse(ex.Message));
            }
        }
    }
}
