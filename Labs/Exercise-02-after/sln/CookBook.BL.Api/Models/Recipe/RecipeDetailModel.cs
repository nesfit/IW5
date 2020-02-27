using AutoMapper;
using CookBook.Common.Enums;
using CookBook.Common.Extensions;
using CookBook.DAL.Entities;
using System;
using System.Collections.Generic;

namespace CookBook.BL.Api.Models.Recipe
{
    public class RecipeDetailModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public FoodType FoodType { get; set; }
        public IList<RecipeDetailIngredientModel> Ingredients { get; set; }
    }

    public class RecipeDetailModelMapperProfile : Profile
    {
        public RecipeDetailModelMapperProfile()
        {
            CreateMap<RecipeEntity, RecipeDetailModel>()
                .MapMember(dst => dst.Ingredients, src => src.IngredientAmounts);
        }
    }
}