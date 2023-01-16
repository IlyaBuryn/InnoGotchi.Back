using FluentValidation;
using InnoGotchi.Components.DtoModels;
using InnoGotchi.Components.DtoValidation.Attributes;

namespace InnoGotchi.Components.DtoValidation
{
    [Validator(typeof(CollaboratorDto))]
    public class ColabDtoValidator : AbstractValidator<CollaboratorDto>
    {
        public ColabDtoValidator()
        {
            RuleFor(c => c.FarmId).NotEmpty().WithMessage("Farm id is required.");
            RuleFor(c => c.IdentityUserId).NotEmpty().WithMessage("User id is required.");
        }
    }
}
