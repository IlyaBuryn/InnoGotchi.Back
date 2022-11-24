using FluentValidation;

namespace InnoGotchi.DataAccess.Models.Validators
{
    public class IdentityRoleValidator : AbstractValidator<IdentityRole>
    {
        public IdentityRoleValidator()
        {
            RuleFor(r => r.Name).NotEmpty().MaximumLength(64);
        }
    }
}
