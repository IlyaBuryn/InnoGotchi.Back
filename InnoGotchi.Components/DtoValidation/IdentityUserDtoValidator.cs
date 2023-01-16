using FluentValidation;
using InnoGotchi.Components.DtoModels;
using InnoGotchi.Components.DtoValidation.Attributes;

namespace InnoGotchi.Components.DtoValidation
{
    [Validator(typeof(IdentityUserDto))]
    public class IdentityUserDtoValidator : AbstractValidator<IdentityUserDto>
    {
        public IdentityUserDtoValidator()
        {
            RuleFor(u => u.Username).NotEmpty().WithMessage("Username is required.");
            RuleFor(u => u.Username).EmailAddress().WithMessage("The property must contain an email address.");
            RuleFor(u => u.Password).NotEmpty().WithMessage("Password is required.");
            RuleFor(u => u.Password).MinimumLength(6).WithMessage("Password must be greater than six characters.");
            RuleFor(u => u.Name).MaximumLength(128).WithMessage("The name must be less than 128 characters.");
            RuleFor(u => u.Surname).MaximumLength(128).WithMessage("The name must be less than 128 characters.");
        }
    }
}
