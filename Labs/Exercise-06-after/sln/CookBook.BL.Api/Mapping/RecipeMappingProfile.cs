using AutoMapper;
using CookBook.DAL.Entities;
using CookBook.Models;

namespace CookBook.BL.Api.Mapping
{
    public class RecipeMappingProfile : Profile
    {
        public RecipeMappingProfile()
        {
            CreateMap<RecipeEntity, RecipeDetailModel>()
                .MapMember(dst => dst.Ingredients, src => src.IngredientAmounts);

            CreateMap<RecipeDetailModel, RecipeEntity>()
                .MapMember(dst => dst.IngredientAmounts, src => src.Ingredients);

            CreateMap<RecipeEntity, RecipeListModel>();

            CreateMap<RecipeListIngredientModel, IngredientAmountEntity>()
                .Ignore(dst => dst.Id)
                .Ignore(dst => dst.RecipeId)
                .Ignore(dst => dst.Recipe)
                .Ignore(dst => dst.Ingredient);

            CreateMap<IngredientAmountEntity, RecipeListIngredientModel>()
                .MapMember(dst => dst.Name, src => src.Ingredient.Name);
        }
    }
}