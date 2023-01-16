using FluentValidation;
using InnoGotchi.Components.DtoModels;
using InnoGotchi.Components.DtoValidation.Attributes;

namespace InnoGotchi.Components.DtoValidation
{
    [Validator(typeof(VitalSignDto))]
    public class VitalSignDtoValidator : AbstractValidator<VitalSignDto>
    {
        public VitalSignDtoValidator()
        {
            RuleFor(v => v.PetId).NotEmpty().WithMessage("Pet id is required.");
            RuleFor(v => v.HungerLevel).NotEmpty().WithMessage("Hunger level is required.");
            RuleFor(v => v.HungerLevel).InclusiveBetween(0, 3).WithMessage("Exceeding the maximum or minimum allowable values.");
            RuleFor(v => v.ThirstyLevel).NotEmpty().WithMessage("Thirsty level is required.");
            RuleFor(v => v.ThirstyLevel).InclusiveBetween(0, 3).WithMessage("Exceeding the maximum or minimum allowable values.");
            RuleFor(v => v.HappinessDaysCount).NotEmpty().WithMessage("Happiness days count is required.");
            RuleFor(v => v.HappinessDaysCount).GreaterThanOrEqualTo(0).WithMessage("Happiness days count must be non-negative.");
        }
    }
}
