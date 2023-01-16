using FluentValidation;
using InnoGotchi.Components.DtoModels;
using InnoGotchi.Components.DtoValidation.Attributes;

namespace InnoGotchi.Components.DtoValidation
{
    [Validator(typeof(BodyPartDto))]
    public class BodyPartDtoValidator : AbstractValidator<BodyPartDto>
    {
        public BodyPartDtoValidator()
        {
            RuleFor(b => b.BodyPartTypeId).NotEmpty().WithMessage("BodyPartType id is required.");
        }
    }
}
