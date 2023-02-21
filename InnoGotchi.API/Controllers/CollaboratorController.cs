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
        private readonly ICollaboratorService _collabService;
        private readonly IValidator<CollaboratorDto> _collabValidator;

        public CollaboratorController(
            ICollaboratorService collabService,
            IValidator<CollaboratorDto> collabValidator)
        {
            _collabService = collabService;
            _collabValidator = collabValidator;
        }


        [HttpPost("create")]
        [Authorize]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddCollabAsync([FromBody] CollaboratorDto collab)
        {
            var validationResult = await _collabValidator.ValidateAsync(collab);
            if (!validationResult.IsValid)
            {
                throw new DataValidationException();
            }
            int? response = await _collabService.CreateCollaboratorAsync(collab);
            return CreatedAtAction(nameof(AddCollabAsync), response);
        }


        [HttpDelete("delete/{userId}")]
        [Authorize]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveCollabAsync([FromRoute] int userId)
        {
            var response = await _collabService.DeleteCollaboratorByUserIdAsync(userId);
            return Ok(response);
        }


        [HttpGet("farm/{farmId}")]
        [Authorize]
        [ProducesResponseType(typeof(List<CollaboratorDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public IActionResult GetCollabsByFarm([FromRoute] int farmId)
        {
            var response = _collabService.GetAllCollaboratorsByFarm(farmId);
            return Ok(response);
        }


        [HttpGet("user/{userId}")]
        [Authorize]
        [ProducesResponseType(typeof(List<CollaboratorDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public IActionResult GetCollabsByUser([FromRoute] int userId)
        {
            var response = _collabService.GetAllCollaboratorsByUser(userId);
            return Ok(response);
        }
    }
}
