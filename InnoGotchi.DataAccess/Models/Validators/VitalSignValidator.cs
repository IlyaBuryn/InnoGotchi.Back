using FluentValidation;

namespace InnoGotchi.DataAccess.Models.Validators
{
    public class VitalSignValidator : AbstractValidator<VitalSign>
    {
        public VitalSignValidator()
        {
            RuleFor(v => v.PetId).NotEmpty();
            RuleFor(v => v.HungerLevel).NotEmpty().InclusiveBetween(0, 3);
            RuleFor(v => v.ThirstyLevel).NotEmpty().InclusiveBetween(0, 3);
            RuleFor(v => v.HappinessDaysCount).NotEmpty().GreaterThanOrEqualTo(0);
        }
    }
}
