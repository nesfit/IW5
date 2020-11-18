using AutoMapper;
using CookBook.DAL.Entities;
using CookBook.Models;

namespace CookBook.BL.Api.MapperProfiles
{
    public class IngredientMapperProfile : Profile
    {
        public IngredientMapperProfile()
        {
            CreateMap<IngredientEntity, IngredientListModel>();
            CreateMap<IngredientEntity, IngredientDetailModel>();

            CreateMap<IngredientDetailModel, IngredientEntity>();
        }
    }
}