using FluentValidation;
using InnoGotchi.Components.DtoModels;
using InnoGotchi.Components.DtoValidation.Attributes;

namespace InnoGotchi.Components.DtoValidation
{
    [Validator(typeof(PetDto))]
    public class PetDtoValidator : AbstractValidator<PetDto>
    {
        public PetDtoValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.Name).MaximumLength(64).WithMessage("{PropertyName} must be less than {MaxLength} characters.");
            RuleFor(p => p.FarmId).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.BodyParts).NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
