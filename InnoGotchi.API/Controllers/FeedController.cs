using InnoGotchi.API.Responses;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.Components.DtoModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchi.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("innogotchi/feed")]
    public class FeedController : ControllerBase
    {
        private readonly IFeedService _feedService;

        public FeedController(IFeedService feedService)
        {
            _feedService = feedService;
        }

        [HttpPost("food")]
        [Authorize]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FeedPet([FromBody] FeedDto feedInfo)
        {
            int? response = await _feedService.FeedPet(feedInfo, BusinessLogic.Services.FeedActionType.Feed);
            return CreatedAtAction(nameof(FeedPet), response);
        }


        [HttpPost("water")]
        [Authorize]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DrinkPet([FromBody] FeedDto feedInfo)
        {
            int? response = await _feedService.FeedPet(feedInfo, BusinessLogic.Services.FeedActionType.Drink);
            return CreatedAtAction(nameof(DrinkPet), response);
        }


        [HttpPost("recalculate/{farmId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RecalculatePetsNeeds([FromRoute] int farmId)
        {
            await _feedService.RecalculatePetsNeeds((int)farmId);
            return Ok();
        }


        [HttpGet("food/{farmId}")]
        [Authorize]
        [ProducesResponseType(typeof(double?), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFeedsFoodInfo([FromRoute] int farmId)
        {
            var response = await _feedService.GetFeedPeriods(farmId, BusinessLogic.Services.FeedActionType.Feed);
            return Ok(response);
        }


        [HttpGet("water/{farmId}")]
        [Authorize]
        [ProducesResponseType(typeof(double?), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFeedsDrinkInfo([FromRoute] int farmId)
        {
            var response = await _feedService.GetFeedPeriods(farmId, BusinessLogic.Services.FeedActionType.Drink);
            return Ok(response);
        }
    }
}
