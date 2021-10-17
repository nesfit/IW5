using System;
using AutoMapper;
using CookBook.Common.Enums;

namespace CookBook.Api.DAL.Common.Entities
{
    public record IngredientAmountEntity
    {
        public Guid? Id { get; set; }
        public double Amount { get; set; }
        public Unit Unit { get; set; }

        public Guid RecipeId { get; set; }
        public RecipeEntity? Recipe { get; set; }

        public Guid IngredientId { get; set; }
        public IngredientEntity? Ingredient { get; set; }

        public IngredientAmountEntity(Guid? id, double amount, Unit unit, Guid recipeId, Guid ingredientId)
        {
            Id = id;
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
