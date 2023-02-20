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
        public async Task<IActionResult> FeedPetAsync([FromBody] FeedDto feedInfo)
        {
            int? response = await _feedService.FeedPetAsync(feedInfo, BusinessLogic.Services.FeedActionType.Feed);
            return CreatedAtAction(nameof(FeedPetAsync), response);
        }


        [HttpPost("water")]
        [Authorize]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DrinkPetAsync([FromBody] FeedDto feedInfo)
        {
            int? response = await _feedService.FeedPetAsync(feedInfo, BusinessLogic.Services.FeedActionType.Drink);
            return CreatedAtAction(nameof(DrinkPetAsync), response);
        }


        [HttpPost("recalculate/{farmId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RecalculatePetsNeedsAsync([FromRoute] int farmId)
        {
            await _feedService.RecalculatePetsNeedsAsync((int)farmId);
            return Ok(true);
        }


        [HttpGet("food/{farmId}")]
        [Authorize]
        [ProducesResponseType(typeof(double?), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public IActionResult GetFeedsFoodInfo([FromRoute] int farmId)
        {
            var response = _feedService.GetFeedPeriods(farmId, BusinessLogic.Services.FeedActionType.Feed);
            return Ok(response);
        }


        [HttpGet("water/{farmId}")]
        [Authorize]
        [ProducesResponseType(typeof(double?), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public IActionResult GetFeedsDrinkInfo([FromRoute] int farmId)
        {
            var response = _feedService.GetFeedPeriods(farmId, BusinessLogic.Services.FeedActionType.Drink);
            return Ok(response);
        }
    }
}
