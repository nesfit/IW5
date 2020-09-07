using System;
using CookBook.Localization.Resources;
using CookBook.Models.Enums;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace CookBook.Models.Recipe
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