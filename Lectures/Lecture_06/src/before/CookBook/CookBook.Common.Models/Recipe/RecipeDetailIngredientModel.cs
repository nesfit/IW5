using CookBook.Common.Enums;
using CookBook.Common.Models.Resources.Texts;
using FluentValidation;

namespace CookBook.Common.Models;

public record RecipeDetailIngredientModel
{
    public required Guid Id { get; set; }
    public required double Amount { get; set; }
    public required Unit Unit { get; set; }
    public required IngredientListModel Ingredient { get; set; }
}

public class RecipeDetailIngredientModelValidator : AbstractValidator<RecipeDetailIngredientModel>
{
    public RecipeDetailIngredientModelValidator()
    {
        const int amountGreaterThan = 0;

        RuleFor(x => x.Amount)
            .GreaterThan(amountGreaterThan)
            .WithMessage(string.Format(RecipeDetailIngredientModelResources.Amount_GreaterThan_ErrorMessage, amountGreaterThan));
    }
}
