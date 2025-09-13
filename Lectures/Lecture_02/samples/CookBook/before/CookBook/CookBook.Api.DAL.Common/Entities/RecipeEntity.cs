using System;
using System.Collections.Generic;
using AutoMapper;
using CookBook.Common.Enums;

namespace CookBook.Api.DAL.Common.Entities
{
    public record RecipeEntity : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public FoodType FoodType { get; set; }
        public string? ImageUrl { get; set; }

        public ICollection<IngredientAmountEntity> IngredientAmounts { get; set; } = new List<IngredientAmountEntity>();

        public RecipeEntity(string name, string description, TimeSpan duration, FoodType foodType, string? imageUrl = null)
        {
            Name = name;
            Description = description;
            Duration = duration;
            FoodType = foodType;
            ImageUrl = imageUrl;
        }
    }

    public class RecipeEntityMapperProfile : Profile
    {
        public RecipeEntityMapperProfile()
        {
            CreateMap<RecipeEntity, RecipeEntity>();
        }
    }
}
