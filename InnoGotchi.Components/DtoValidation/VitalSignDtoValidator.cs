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
            RuleFor(v => v.PetId).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(v => v.HungerLevel).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(v => v.HungerLevel).InclusiveBetween(0, 3).WithMessage("Exceeding the maximum ({To}) or minimum ({From}) allowable values.");
            RuleFor(v => v.ThirstyLevel).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(v => v.ThirstyLevel).InclusiveBetween(0, 3).WithMessage("Exceeding the maximum ({To}) or minimum ({From}) allowable values.");
            RuleFor(v => v.HappinessDaysCount).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(v => v.HappinessDaysCount).GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be greater than or equal to {ComparisonValue}.");
        }
    }
}
