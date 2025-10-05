using CookBook.Common.Enums;
using CookBook.Common.Models.Resources.Texts;
using FluentValidation;

namespace CookBook.Common.Models;

public record RecipeDetailModel : IWithId
{
    public required Guid Id { get; init; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public string? ImageUrl { get; set; }
    public required TimeSpan Duration { get; set; }
    public required FoodType FoodType { get; set; }
    public IList<RecipeDetailIngredientModel> IngredientAmounts { get; set; } = new List<RecipeDetailIngredientModel>();
}

public class RecipeDetailModelValidator : AbstractValidator<RecipeDetailModel>
{
    public RecipeDetailModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(RecipeDetailModelResources.Name_Required_ErrorMessage);

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage(RecipeDetailModelResources.Description_Required_ErrorMessage)
            .MinimumLength(10)
            .WithMessage(RecipeDetailModelResources.Description_MinLength_ErrorMessage);

        RuleForEach(x => x.IngredientAmounts)
            .SetValidator(new RecipeDetailIngredientModelValidator());
    }
}
