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

            CreateMap<RecipeEntity, RecipeListModel>();

            CreateMap<RecipeNewIngredientModel, IngredientAmountEntity>()
                .Ignore(dst => dst.Id)
                .Ignore(dst => dst.RecipeId)
                .Ignore(dst => dst.Recipe)
                .Ignore(dst => dst.Ingredient);

            CreateMap<RecipeNewModel, RecipeEntity>()
                .MapMember(dst => dst.IngredientAmounts, src => src.Ingredients)
                .Ignore(dst => dst.Id);

            CreateMap<RecipeUpdateIngredientModel, IngredientAmountEntity>()
                .Ignore(dst => dst.Id)
                .Ignore(dst => dst.RecipeId)
                .Ignore(dst => dst.Recipe)
                .Ignore(dst => dst.Ingredient);


            CreateMap<RecipeUpdateModel, RecipeEntity>()
                .MapMember(dst => dst.IngredientAmounts, src => src.Ingredients);
        }
    }
}