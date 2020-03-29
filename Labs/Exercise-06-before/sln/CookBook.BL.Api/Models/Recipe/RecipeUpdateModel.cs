using AutoMapper;
using CookBook.Common.Enums;
using CookBook.Common.Extensions;
using CookBook.DAL.Entities;
using System;
using System.Collections.Generic;

namespace CookBook.BL.Api.Models.Recipe
{
    public class RecipeUpdateModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public FoodType FoodType { get; set; }
        public IList<RecipeUpdateIngredientModel> Ingredients { get; set; }
    }

    public class RecipeUpdateModelMapperProfile : Profile
    {
        public RecipeUpdateModelMapperProfile()
        {
            CreateMap<RecipeUpdateModel, RecipeEntity>()
                .MapMember(dst => dst.IngredientAmounts, src => src.Ingredients);
        }
    }
}