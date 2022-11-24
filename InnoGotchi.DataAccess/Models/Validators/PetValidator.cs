using FluentValidation;

namespace InnoGotchi.DataAccess.Models.Validators
{
    public class PetValidator : AbstractValidator<Pet>
    {
        public PetValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(64);
            RuleFor(p => p.FarmId).NotEmpty();
        }
    }
}
