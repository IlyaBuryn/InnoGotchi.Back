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
            RuleFor(r => r.Name).NotEmpty().WithMessage("Role name is required.");
            RuleFor(r => r.Name).MaximumLength(64).WithMessage("The name must be less than 64 characters.");
        }
    }
}
