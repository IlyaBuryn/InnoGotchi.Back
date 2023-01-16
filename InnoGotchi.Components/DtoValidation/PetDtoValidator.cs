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
            RuleFor(p => p.Name).NotEmpty().WithMessage("Pet name is required.");
            RuleFor(p => p.Name).MaximumLength(64).WithMessage("The name must be less than 64 characters.");
            RuleFor(p => p.FarmId).NotEmpty().WithMessage("Farm id is required."); ;
        }
    }
}
