using FluentValidation;
using InnoGotchi.DataAccess.Models;

namespace InnoGotchi.DataAccess.Validators
{
    public class IdentityUserValidator : AbstractValidator<IdentityUser>
    {
        public IdentityUserValidator()
        {
            RuleFor(u => u.Username).NotNull().EmailAddress();
            RuleFor(u => u.Password).NotEmpty().MinimumLength(6);
            RuleFor(u => u.Name).MaximumLength(127);
            RuleFor(u => u.Surname).MaximumLength(127);
            RuleFor(u => u.Role).NotNull();
        }
    }
}
