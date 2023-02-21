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
    [Route("innogotchi/vital-sign")]
    public class VitalSignController : ControllerBase
    {
        private readonly IVitalSignService _vitalSignService;
        private readonly IValidator<VitalSignDto> _vsValidator;

        public VitalSignController(IVitalSignService vitalSignService,
            IValidator<VitalSignDto> vsValidator)
        {
            _vitalSignService = vitalSignService;
            _vsValidator = vsValidator;
        }

        [HttpPost("create")]
        [Authorize]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddVitalSignAsync([FromBody] VitalSignDto vitalSign)
        {
            var validationResult = await _vsValidator.ValidateAsync(vitalSign);
            if (!validationResult.IsValid)
            {
                throw new DataValidationException();
            }
            int? response = await _vitalSignService.CreateVitalSignAsync(vitalSign);
            return CreatedAtAction(nameof(AddVitalSignAsync), response);
        }


        [HttpPut("update")]
        [Authorize]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateVitalSignAsync([FromBody] VitalSignDto vitalSign)
        {
            var validationResult = await _vsValidator.ValidateAsync(vitalSign);
            if (!validationResult.IsValid)
            {
                throw new DataValidationException();
            }
            var response = await _vitalSignService.UpdateVitalSignAsync(vitalSign);
            return Ok(response);
        }


        [HttpGet("{vsId}")]
        [Authorize]
        [ProducesResponseType(typeof(VitalSignDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public IActionResult GetVitalSignById([FromRoute] int vsId)
        {
            var response = _vitalSignService.GetVitalSignById(vsId);
            return Ok(response);
        }


        [HttpGet("pet/{petId}")]
        [Authorize]
        [ProducesResponseType(typeof(VitalSignDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public IActionResult GetVitalSignByPetId([FromRoute] int petId)
        {
            var response = _vitalSignService.GetVitalSignByPetId(petId);
            return Ok(response);
        }
    }
}
