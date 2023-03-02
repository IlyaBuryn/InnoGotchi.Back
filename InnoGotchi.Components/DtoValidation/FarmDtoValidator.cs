using FluentValidation;
using InnoGotchi.Components.DtoModels;
using InnoGotchi.Components.DtoValidation.Attributes;

namespace InnoGotchi.Components.DtoValidation
{
    [Validator(typeof(FarmDto))]
    public class FarmDtoValidator : AbstractValidator<FarmDto>
    {
        public FarmDtoValidator()
        {
            RuleFor(f => f.Name).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(f => f.Name).MaximumLength(128).WithMessage("{PropertyName} must be less than {MaxLength} characters.");
            RuleFor(f => f.IdentityUserId).NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
