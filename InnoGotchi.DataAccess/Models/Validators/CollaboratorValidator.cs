using FluentValidation;

namespace InnoGotchi.DataAccess.Models.Validators
{
    public class CollaboratorValidator : AbstractValidator<Collaborator>
    {
        public CollaboratorValidator()
        {
            RuleFor(c => c.FarmId).NotEmpty();
            RuleFor(c => c.IdentityUserId).NotEmpty();
        }
    }
}
