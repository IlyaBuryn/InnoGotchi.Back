using FluentValidation;
using InnoGotchi.Components.DtoModels;
using InnoGotchi.Components.DtoValidation.Attributes;

namespace InnoGotchi.Components.DtoValidation
{
    [Validator(typeof(AuthenticateRequestDto))]
    public class AuthenticateRequestDtoValidator : AbstractValidator<AuthenticateRequestDto>
    {
        public AuthenticateRequestDtoValidator()
        {
            RuleFor(u => u.Username).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(u => u.Username).EmailAddress().WithMessage("{PropertyName} must contain an email address.");
            RuleFor(u => u.Password).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(u => u.Password).MinimumLength(6).WithMessage("{PropertyName} must be greater than {MinLength} characters.");
            RuleFor(u => u.Name).MaximumLength(128).WithMessage("{PropertyName} must be less than {MaxLength} characters.");
            RuleFor(u => u.Surname).MaximumLength(128).WithMessage("{PropertyName} must be less than {MaxLength} characters.");
        }
    }
}
