using FluentValidation;

namespace InnoGotchi.DataAccess.Models.Validators
{
    public class FeedValidator : AbstractValidator<Feed>
    {
        public FeedValidator()
        {
            RuleFor(p => p.FoodCount).InclusiveBetween(0, 3);
            RuleFor(p => p.WaterCount).InclusiveBetween(0, 3);
        }
    }
}
