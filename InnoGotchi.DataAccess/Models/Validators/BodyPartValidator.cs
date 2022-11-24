using FluentValidation;

namespace InnoGotchi.DataAccess.Models.Validators
{
    public class BodyPartValidator : AbstractValidator<BodyPart>
    {
        public BodyPartValidator()
        {
            RuleFor(b => b.BodyPartTypeId).NotEmpty();
        }
    }
}
