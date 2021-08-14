using AutoMapper;
using CookBook.Common.Enums;
using System;

namespace CookBook.DAL.Api.Entities
{
    public record IngredientAmountEntity : EntityBase
    {
        public double Amount { get; set; }
        public Unit Unit { get; set; }

        public Guid RecipeId { get; set; }
        public RecipeEntity? Recipe { get; set; }

        public Guid IngredientId { get; set; }
        public IngredientEntity? Ingredient { get; set; }

        public IngredientAmountEntity(double amount, Unit unit, Guid recipeId, Guid ingredientId)
        {
            Amount = amount;
            Unit = unit;
            RecipeId = recipeId;
            IngredientId = ingredientId;
        }
    }

    public class IngredientAmountEntityMapperProfile : Profile
    {
        public IngredientAmountEntityMapperProfile()
        {
            CreateMap<IngredientAmountEntity, IngredientAmountEntity>();
        }
    }
}
