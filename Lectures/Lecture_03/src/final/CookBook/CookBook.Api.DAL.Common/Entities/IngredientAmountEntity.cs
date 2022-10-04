using System;
using AutoMapper;
using CookBook.Common.Enums;

namespace CookBook.Api.DAL.Common.Entities
{
    public record IngredientAmountEntity : EntityBase
    {
        public required double Amount { get; set; }
        public required Unit Unit { get; set; }

        public Guid RecipeId { get; set; }
        public RecipeEntity? Recipe { get; set; }

        public Guid IngredientId { get; set; }
        public IngredientEntity? Ingredient { get; set; }
    }

    public class IngredientAmountEntityMapperProfile : Profile
    {
        public IngredientAmountEntityMapperProfile()
        {
            CreateMap<IngredientAmountEntity, IngredientAmountEntity>();
        }
    }
}
