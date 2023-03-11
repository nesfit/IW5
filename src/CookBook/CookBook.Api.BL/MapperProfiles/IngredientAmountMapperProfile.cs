using AutoMapper;
using CookBook.Api.DAL.Common.Entities;
using CookBook.Common.Extensions;
using CookBook.Common.Models;

namespace CookBook.Api.BL.MapperProfiles
{
    public class IngredientAmountMapperProfile : Profile
    {
        public IngredientAmountMapperProfile()
        {
            CreateMap<IngredientAmountEntity, RecipeDetailIngredientModel>();
            
            CreateMap<RecipeDetailIngredientModel, IngredientAmountEntity>()
                .Ignore(dst => dst.Ingredient)
                .Ignore(dst => dst.Recipe)
                .Ignore(dst => dst.RecipeId);
        }
    }
}
