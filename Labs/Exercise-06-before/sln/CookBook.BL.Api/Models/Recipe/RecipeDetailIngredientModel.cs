using AutoMapper;
using CookBook.Common.Enums;
using CookBook.Common.Extensions;
using CookBook.DAL.Entities;
using EnumsNET;
using System;

namespace CookBook.BL.Api.Models.Recipe
{
    public class RecipeDetailIngredientModel
    {
        public Guid IngredientId { get; set; }
        public double Amount { get; set; }
        public Unit Unit { get; set; }
        public string Name { get; set; }
        public string UnitText => Unit.AsString(EnumFormat.Description);
    }

    public class RecipeDetailIngredientModelMapperProfile : Profile
    {
        public RecipeDetailIngredientModelMapperProfile()
        {
            CreateMap<IngredientAmountEntity, RecipeDetailIngredientModel>()
                .MapMember(dst => dst.Name, src => src.Ingredient.Name);
        }
    }
}