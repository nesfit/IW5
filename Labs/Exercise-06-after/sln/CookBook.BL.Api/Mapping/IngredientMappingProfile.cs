using AutoMapper;
using CookBook.DAL.Entities;
using CookBook.Models;

namespace CookBook.BL.Api.Mapping
{
    public class IngredientMappingProfile : Profile
    {
        public IngredientMappingProfile()
        {
            CreateMap<IngredientEntity, IngredientDetailModel>();

            CreateMap<IngredientDetailModel, IngredientEntity>();

            CreateMap<IngredientEntity, IngredientListModel>();
        }
    }
}
