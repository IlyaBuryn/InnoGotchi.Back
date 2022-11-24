using FluentValidation;

namespace InnoGotchi.DataAccess.Models.Validators
{
    public class IdentityUserValidator : AbstractValidator<IdentityUser>
    {
        public IdentityUserValidator()
        {
            RuleFor(u => u.Username).NotEmpty().EmailAddress()
                .WithMessage("The property must contain an email address!");
            RuleFor(u => u.Password).NotEmpty().MinimumLength(6)
                .WithMessage("Password must be greater than six characters!");
            RuleFor(u => u.Name).MaximumLength(128);
            RuleFor(u => u.Surname).MaximumLength(128);
        }
    }
}
