using AutoMapper;
using CookBook.Common.Enums;
using CookBook.Common.Extensions;
using CookBook.DAL.Entities;
using System;

namespace CookBook.BL.Api.Models.Recipe
{
    public class RecipeUpdateIngredientModel
    {
        public Guid IngredientId { get; set; }
        public double Amount { get; set; }
        public Unit Unit { get; set; }
    }

    public class RecipeUpdateIngredientModelMapperProfile : Profile
    {
        public RecipeUpdateIngredientModelMapperProfile()
        {
            CreateMap<RecipeUpdateIngredientModel, IngredientAmountEntity>()
                .Ignore(dst => dst.Id)
                .Ignore(dst => dst.RecipeId)
                .Ignore(dst => dst.Recipe)
                .Ignore(dst => dst.Ingredient);
        }
    }
}