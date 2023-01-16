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
            RuleFor(f => f.Name).NotEmpty().WithMessage("Farm name is required.");
            RuleFor(f => f.Name).MaximumLength(128).WithMessage("The name must be less than 64 characters.");
            RuleFor(f => f.IdentityUserId).NotEmpty().WithMessage("User id is required.");
        }
    }
}
