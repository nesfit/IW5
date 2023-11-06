using AutoMapper;
using CookBook.Api.DAL.Common.Entities;
using CookBook.Common.Extensions;
using CookBook.Common.Models;

namespace CookBook.Api.BL.MapperProfiles
{
    public class RecipeMapperProfile : Profile
    {
        public RecipeMapperProfile()
        {
            CreateMap<RecipeEntity, RecipeListModel>();
            CreateMap<RecipeEntity, RecipeDetailModel>()
                .MapMember(model => model.IngredientAmounts, entity => entity.IngredientAmounts);
            CreateMap<IngredientAmountEntity, RecipeDetailIngredientModel>();

            CreateMap<RecipeDetailModel, RecipeEntity>()
                .Ignore(entity => entity.UserId)
                .Ignore(entity => entity.IngredientAmounts);
        }
    }
}
