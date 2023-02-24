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
    [Route("innogotchi/collab")]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorService _collaboratorService;

        public CollaboratorController(
            ICollaboratorService collaboratorService)
        {
            _collaboratorService = collaboratorService;
        }


        [HttpPost("create")]
        [Authorize]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddCollabAsync([FromBody] CollaboratorDto collaborator)
        {
            if (!ModelState.IsValid)
            {
                throw new DataValidationException();
            }
            int? response = await _collaboratorService.CreateCollaboratorAsync(collaborator);
            return CreatedAtAction(nameof(AddCollabAsync), response);
        }


        [HttpDelete("delete/{userId}")]
        [Authorize]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveCollabAsync([FromRoute] int userId)
        {
            var response = await _collaboratorService.DeleteCollaboratorByUserIdAsync(userId);
            return Ok(response);
        }


        [HttpGet("farm/{farmId}")]
        [Authorize]
        [ProducesResponseType(typeof(List<CollaboratorDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCollabsByFarmAsync([FromRoute] int farmId)
        {
            var response = await _collaboratorService.GetAllCollaboratorsByFarmAsync(farmId);
            return Ok(response);
        }


        [HttpGet("user/{userId}")]
        [Authorize]
        [ProducesResponseType(typeof(List<CollaboratorDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCollabsByUserAsync([FromRoute] int userId)
        {
            var response = await _collaboratorService.GetAllCollaboratorsByUserAsync(userId);
            return Ok(response);
        }
    }
}
