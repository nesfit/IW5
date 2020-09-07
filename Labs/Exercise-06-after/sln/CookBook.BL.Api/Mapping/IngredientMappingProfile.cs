using AutoMapper;
using CookBook.DAL.Entities;
using CookBook.Models.Ingredient;
using CookBook.Models.Recipe;

namespace CookBook.BL.Api.Mapping
{
    public class IngredientMappingProfile : Profile
    {
        public IngredientMappingProfile()
        {
            CreateMap<IngredientEntity, IngredientDetailModel>();

            CreateMap<IngredientEntity, IngredientListModel>();

            CreateMap<IngredientNewModel, IngredientEntity>()
                .Ignore(dst => dst.Id);

            CreateMap<IngredientUpdateModel, IngredientEntity>();

            CreateMap<IngredientAmountEntity, RecipeDetailIngredientModel>()
                .MapMember(dst => dst.Name, src => src.Ingredient.Name);
        }
    }
}
