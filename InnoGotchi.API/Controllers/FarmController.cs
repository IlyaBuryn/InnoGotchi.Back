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
    [Route("innogotchi/farm")]
    public class FarmController : ControllerBase
    {
        private readonly IFarmService _farmService;
        private readonly IFeedService _feedService;
        private readonly IValidator<FarmDto> _farmValidator;

        public FarmController(
            IFarmService farmService, 
            IFeedService feedService,
            IValidator<FarmDto> farmValidator)
        {
            _farmService = farmService;
            _feedService = feedService;
            _farmValidator = farmValidator;
        }


        [HttpPost("create")]
        [Authorize]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateFarmAsync([FromBody] FarmDto farm)
        {
            var validationResult = await _farmValidator.ValidateAsync(farm);
            if (!validationResult.IsValid)
            {
                throw new DataValidationException();
            }
            int? response = await _farmService.CreateFarmAsync(farm);
            return CreatedAtAction(nameof(CreateFarmAsync), response);
        }


        [HttpDelete("delete/{id}")]
        [Authorize]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteFarmAsync([FromRoute] int id)
        {
            var response = await _farmService.DeleteFarmAsync(id);
            return Ok(response);
        }


        [HttpPut("update")]
        [Authorize]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateFarmAsync([FromBody] FarmDto farmToUpdate)
        {
            var validationResult = await _farmValidator.ValidateAsync(farmToUpdate);
            if (!validationResult.IsValid)
            {
                throw new DataValidationException();
            }
            var response = await _farmService.UpdateFarmAsync(farmToUpdate);
            return Ok(response);
        }


        [HttpGet("{farmId}")]
        [Authorize]
        [ProducesResponseType(typeof(FarmDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFarmByIdAsync([FromRoute] int farmId)
        {
            var response = await _farmService.GetFarmByIdAsync(farmId);
            if (response != null && response.Id != 0)
                await _feedService.RecalculatePetsNeedsAsync(response.Id);

            return Ok(response);
        }


        [HttpGet("user/{userId}")]
        [Authorize]
        [ProducesResponseType(typeof(FarmDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFarmByUserIdAsync([FromRoute] int userId)
        {
            var response = await _farmService.GetFarmByUserIdAsync(userId);
            if (response != null && response.Id != 0)
                await _feedService.RecalculatePetsNeedsAsync(response.Id);

            return Ok(response);
        }
    }
}
