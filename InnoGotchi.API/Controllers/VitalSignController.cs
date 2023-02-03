using InnoGotchi.API.Responses;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.Components.DtoModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchi.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("innogotchi/vital-sign")]
    public class VitalSignController : ControllerBase
    {
        private readonly IVitalSignService _vitalSignService;

        public VitalSignController(IVitalSignService vitalSignService)
        {
            _vitalSignService = vitalSignService;
        }

        [HttpPost("create")]
        [Authorize]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddVitalSign([FromBody] VitalSignDto vitalSign)
        {
            int? response = await _vitalSignService.CreateVitalSignAsync(vitalSign);
            return CreatedAtAction(nameof(AddVitalSign), response);
        }


        [HttpPut("update")]
        [Authorize]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateVitalSign([FromBody] VitalSignDto vitalSign)
        {
            var response = await _vitalSignService.UpdateVitalSignAsync(vitalSign);
            return Ok(response);
        }


        [HttpGet("{vsId}")]
        [Authorize]
        [ProducesResponseType(typeof(VitalSignDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVitalSignById([FromRoute] int vsId)
        {
            var response = await _vitalSignService.GetVitalSignByIdAsync(vsId);
            return Ok(response);
        }


        [HttpGet("pet/{petId}")]
        [Authorize]
        [ProducesResponseType(typeof(VitalSignDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVitalSignByPetId([FromRoute] int petId)
        {
            var response = await _vitalSignService.GetVitalSignByPetIdAsync(petId);
            return Ok(response);
        }
    }
}
