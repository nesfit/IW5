using AutoMapper;
using CookBook.Common.Enums;
using CookBook.Common.Extensions;
using CookBook.Common.Resources;
using CookBook.DAL.Entities;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System;

namespace CookBook.BL.Api.Models.Recipe
{
    public class RecipeNewIngredientModel
    {
        public Guid IngredientId { get; set; }
        public double Amount { get; set; }
        public Unit Unit { get; set; }
    }

    public class RecipeNewIngredientModelMapperProfile : Profile
    {
        public RecipeNewIngredientModelMapperProfile()
        {
            CreateMap<RecipeNewIngredientModel, IngredientAmountEntity>()
                .Ignore(dst => dst.Id)
                .Ignore(dst => dst.RecipeId)
                .Ignore(dst => dst.Recipe)
                .Ignore(dst => dst.Ingredient);
        }
    }

    public class RecipeNewIngredientModelValidator : AbstractValidator<RecipeNewIngredientModel>
    {
        private double amountMinimum = 0;

        public RecipeNewIngredientModelValidator(IStringLocalizer<RecipeNewModelResources> recipeNewModeLocalizer)
        {
            RuleFor(item => item.Amount)
                .GreaterThan(amountMinimum)
                .WithMessage(recipeNewModeLocalizer[nameof(RecipeNewModelResources.Ingredients_Amount_ValidationMessage), amountMinimum]);

            //RuleFor(item => item.Unit)
            //    .NotEqual(Unit.Unknown)
            //    .WithMessage(recipeNewModeLocalizer[nameof(RecipeNewModelResources.)])
        }
    }
}