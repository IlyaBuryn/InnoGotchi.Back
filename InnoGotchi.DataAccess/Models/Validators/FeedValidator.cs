using FluentValidation;

namespace InnoGotchi.DataAccess.Models.Validators
{
    public class FeedValidator : AbstractValidator<Feed>
    {
        public FeedValidator()
        {
            RuleFor(p => p.FoodCount).NotEmpty().InclusiveBetween(1, 3);
            RuleFor(p => p.WaterCount).NotEmpty().InclusiveBetween(1, 3);
        }
    }
}
