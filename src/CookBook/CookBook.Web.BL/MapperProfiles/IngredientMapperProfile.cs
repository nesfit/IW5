using AutoMapper;
using CookBook.Common.Models;

namespace CookBook.Web.BL.MapperProfiles
{
    public class IngredientMapperProfile : Profile
    {
        public IngredientMapperProfile()
        {
            CreateMap<IngredientDetailModel, IngredientListModel>();
        }
    }
}