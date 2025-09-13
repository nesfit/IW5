using System;
using System.Collections.Generic;
using AutoMapper;
using CookBook.Common.Enums;

namespace CookBook.Api.DAL.Common.Entities
{
    public record RecipeEntity : EntityBase
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required TimeSpan Duration { get; set; }
        public required FoodType FoodType { get; set; }
        public string? ImageUrl { get; set; }

        public ICollection<IngredientAmountEntity> IngredientAmounts { get; set; } = new List<IngredientAmountEntity>();
    }

    public class RecipeEntityMapperProfile : Profile
    {
        public RecipeEntityMapperProfile()
        {
            CreateMap<RecipeEntity, RecipeEntity>();
        }
    }
}
