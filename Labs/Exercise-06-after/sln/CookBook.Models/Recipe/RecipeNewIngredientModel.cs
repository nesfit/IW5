using CookBook.Localization.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System;

namespace CookBook.Models
{
    public class RecipeNewIngredientModel
    {
        public Guid IngredientId { get; set; }
        public double Amount { get; set; }
        public Unit Unit { get; set; }
    }
    public class RecipeNewIngredientModelValidator : AbstractValidator<RecipeNewIngredientModel>
    {
        private double amountMinimum = 0;

        public RecipeNewIngredientModelValidator(IStringLocalizer<RecipeNewModelResources> recipeNewModeLocalizer)
        {
            RuleFor(item => item.Amount)
                .GreaterThan(amountMinimum)
                .WithMessage(recipeNewModeLocalizer[nameof(RecipeNewModelResources.Ingredients_Amount_ValidationMessage), amountMinimum]);

            RuleFor(item => item.Unit)
                .NotEqual(Unit.Unknown)
                .WithMessage(recipeNewModeLocalizer[nameof(RecipeNewModelResources.Ingredients_Unit_ValidationMessage)]);
        }
    }
}