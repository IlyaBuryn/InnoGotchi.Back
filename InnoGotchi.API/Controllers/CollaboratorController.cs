using InnoGotchi.API.Responses;
using InnoGotchi.BusinessLogic.Dto;
using InnoGotchi.BusinessLogic.Exceptions;
using InnoGotchi.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchi.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("innogotchi/[controller]")]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorService _collabService;

        public CollaboratorController(ICollaboratorService collabService)
        {
            _collabService = collabService;
        }


        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddCollab([FromBody] CollaboratorDto collab)
        {
            try
            {
                int? response = await _collabService.CreateCollaboratorAsync(collab);
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

        [HttpDelete("{userId}")]
        [Authorize]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveCollab([FromRoute] int userId)
        {
            try
            {
                var response = await _collabService.DeleteCollaboratorByUserIdAsync(userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorResponse(ex.Message));
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(List<CollaboratorDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCollabs([FromRoute] int id)
        {
            try
            {
                var response = await _collabService.GetAllCollaboratorsByFarmAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorResponse(ex.Message));
            }
        }
    }
}
