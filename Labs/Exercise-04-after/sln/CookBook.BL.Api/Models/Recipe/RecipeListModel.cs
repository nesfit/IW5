using AutoMapper;
using CookBook.Common.Enums;
using CookBook.DAL.Entities;
using System;

namespace CookBook.BL.Api.Models.Recipe
{
    public class RecipeListModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public FoodType FoodType { get; set; }
    }

    public class RecipeListModelMapperProfile : Profile
    {
        public RecipeListModelMapperProfile()
        {
            CreateMap<RecipeEntity, RecipeListModel>();
        }
    }
}