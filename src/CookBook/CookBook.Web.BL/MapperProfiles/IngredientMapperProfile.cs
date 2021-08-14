using AutoMapper;
using CookBook.Models;

namespace CookBook.BL.Web.MapperProfiles
{
    public class IngredientMapperProfile : Profile
    {
        public IngredientMapperProfile()
        {
            CreateMap<IngredientDetailModel, IngredientListModel>();
        }
    }
}