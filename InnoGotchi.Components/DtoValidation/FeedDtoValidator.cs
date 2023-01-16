using FluentValidation;
using InnoGotchi.Components.DtoModels;
using InnoGotchi.Components.DtoValidation.Attributes;

namespace InnoGotchi.Components.DtoValidation
{
    [Validator(typeof(FeedDto))]
    public class FeedDtoValidator : AbstractValidator<FeedDto>
    {
        public FeedDtoValidator()
        {
            RuleFor(p => p.FoodCount).InclusiveBetween(0, 3).WithMessage("Exceeding the maximum or minimum allowable values.");
            RuleFor(p => p.WaterCount).InclusiveBetween(0, 3).WithMessage("Exceeding the maximum or minimum allowable values.");
        }
    }
}
