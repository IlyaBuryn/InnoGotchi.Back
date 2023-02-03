using InnoGotchi.API.Responses;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.Components.DtoModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchi.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("innogotchi/collab")]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorService _collabService;

        public CollaboratorController(ICollaboratorService collabService)
        {
            _collabService = collabService;
        }


        [HttpPost("create")]
        [Authorize]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddCollab([FromBody] CollaboratorDto collab)
        {
            int? response = await _collabService.CreateCollaboratorAsync(collab);
            return CreatedAtAction(nameof(AddCollab), response);
        }


        [HttpDelete("delete/{userId}")]
        [Authorize]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveCollab([FromRoute] int userId)
        {
            var response = await _collabService.DeleteCollaboratorByUserIdAsync(userId);
            return Ok(response);
        }


        [HttpGet("farm/{farmId}")]
        [Authorize]
        [ProducesResponseType(typeof(List<CollaboratorDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCollabsByFarm([FromRoute] int farmId)
        {
            var response = await _collabService.GetAllCollaboratorsByFarmAsync(farmId);
            return Ok(response);
        }


        [HttpGet("user/{userId}")]
        [Authorize]
        [ProducesResponseType(typeof(List<CollaboratorDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCollabsByUser([FromRoute] int userId)
        {
            var response = await _collabService.GetAllCollaboratorsByUserAsync(userId);
            return Ok(response);
        }
    }
}
