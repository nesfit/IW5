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
                .MapMember(dst => dst.IngredientAmounts, src => src.IngredientAmounts);
            CreateMap<IngredientAmountEntity, RecipeDetailIngredientModel>();

            CreateMap<RecipeDetailModel, RecipeEntity>()
                .Ignore(dst => dst.IngredientAmounts);
        }
    }
}
