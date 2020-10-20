using AutoMapper;
using CookBook.Models;
using System;
using System.Collections.Generic;

namespace CookBook.DAL.Entities
{
    public class RecipeEntity : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public FoodType FoodType { get; set; }
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