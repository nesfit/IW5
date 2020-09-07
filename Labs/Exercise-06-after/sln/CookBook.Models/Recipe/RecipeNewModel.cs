using System;
using System.Collections.Generic;
using CookBook.Localization.Resources;
using CookBook.Models.Enums;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace CookBook.Models.Recipe
{
    public class RecipeNewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public FoodType FoodType { get; set; }
        public IList<RecipeNewIngredientModel> Ingredients { get; set; }
    }

    public class RecipeNewModelValidator : AbstractValidator<RecipeNewModel>
    {
        private const int nameMinimumLength = 3;
        private const int descriptionMinimumLength = 10;
        private const int durationMinimumMinutes = 1;

        public RecipeNewModelValidator(IStringLocalizer<RecipeNewModelResources> recipeNewModeLocalizer)
        {
            RuleFor(recipe => recipe.Name)
                .NotEmpty()
                .MinimumLength(nameMinimumLength)
                .WithMessage(recipeNewModeLocalizer[nameof(RecipeNewModelResources.Name_ValidationMessage), nameMinimumLength].Value);

            RuleFor(recipe => recipe.Description)
                .NotEmpty()
                .MinimumLength(descriptionMinimumLength)
                .WithMessage(recipeNewModeLocalizer[nameof(RecipeNewModelResources.Description_ValidationMessage), descriptionMinimumLength].Value);

            RuleFor(recipe => recipe.FoodType)
                .NotEqual(FoodType.Unknown)
                .WithMessage(recipeNewModeLocalizer[nameof(RecipeNewModelResources.FoodType_ValidationMessage), FoodType.Unknown.ToString()].Value);

            RuleFor(recipe => recipe.Duration)
                .GreaterThan(TimeSpan.FromMinutes(durationMinimumMinutes))
                .WithMessage(recipeNewModeLocalizer[nameof(RecipeNewModelResources.Duration_ValidationMessage), durationMinimumMinutes]);

            RuleFor(recipe => recipe.Ingredients)
                .NotEmpty()
                .WithMessage(recipeNewModeLocalizer[nameof(RecipeNewModelResources.Ingredients_ValidationMessage)].Value);

            //RuleForEach(recipe => recipe.Ingredients)
            //    .SetValidator()
        }
    }
}