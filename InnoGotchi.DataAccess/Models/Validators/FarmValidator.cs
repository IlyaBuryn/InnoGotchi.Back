using FluentValidation;

namespace InnoGotchi.DataAccess.Models.Validators
{
    public class FarmValidator : AbstractValidator<Farm>
    {
        public FarmValidator()
        {
            RuleFor(f => f.Name).NotEmpty().MaximumLength(128);
            RuleFor(f => f.IdentityUserId).NotEmpty();
        }
    }
}
