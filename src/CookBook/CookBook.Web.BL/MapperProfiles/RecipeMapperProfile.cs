using AutoMapper;
using CookBook.Models;

namespace CookBook.BL.Web.MapperProfiles
{
    public class RecipeMapperProfile : Profile
    {
        public RecipeMapperProfile()
        {
            CreateMap<RecipeDetailModel, RecipeListModel>();
        }
    }
}