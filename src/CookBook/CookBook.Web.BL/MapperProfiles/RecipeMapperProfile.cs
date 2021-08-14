using AutoMapper;
using CookBook.Common.Models;

namespace CookBook.Web.BL.MapperProfiles
{
    public class RecipeMapperProfile : Profile
    {
        public RecipeMapperProfile()
        {
            CreateMap<RecipeDetailModel, RecipeListModel>();
        }
    }
}