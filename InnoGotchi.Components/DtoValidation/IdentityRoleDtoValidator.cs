using FluentValidation;
using InnoGotchi.Components.DtoModels;
using InnoGotchi.Components.DtoValidation.Attributes;

namespace InnoGotchi.Components.DtoValidation
{
    [Validator(typeof(IdentityRoleDto))]
    public class IdentityRoleDtoValidator : AbstractValidator<IdentityRoleDto>
    {
        public IdentityRoleDtoValidator()
        {
            RuleFor(r => r.Name).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(r => r.Name).MaximumLength(64).WithMessage("{PropertyName} must be less than {MaxLength} characters.");
        }
    }
}
