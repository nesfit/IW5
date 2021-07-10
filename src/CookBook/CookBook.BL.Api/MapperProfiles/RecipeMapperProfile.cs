using AutoMapper;
using CookBook.Common.Extensions;
using CookBook.DAL.Api.Entities;
using CookBook.Models;

namespace CookBook.BL.Api.MapperProfiles
{
    public class RecipeMapperProfile : Profile
    {
        public RecipeMapperProfile()
        {
            CreateMap<RecipeEntity, RecipeListModel>();
            CreateMap<RecipeEntity, RecipeDetailModel>()
                .MapMember(dst => dst.Ingredients, src => src.IngredientAmounts);
            CreateMap<IngredientAmountEntity, RecipeDetailIngredientModel>();

            CreateMap<RecipeDetailModel, RecipeEntity>()
                .MapMember(dst => dst.IngredientAmounts, src => src.Ingredients);
            CreateMap<RecipeDetailIngredientModel, IngredientAmountEntity>()
                .Ignore(dst => dst.Id)
                .Ignore(dst => dst.RecipeId)
                .Ignore(dst => dst.Recipe)
                .Ignore(dst => dst.Ingredient);
        }
    }
}