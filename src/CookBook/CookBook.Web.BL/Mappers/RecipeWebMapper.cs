using CookBook.Common.Models;
using Riok.Mapperly.Abstractions;

namespace CookBook.Web.BL.Mappers
{
    [Mapper]
    public partial class RecipeWebMapper : IWebMapper<RecipeDetailModel, RecipeListModel>
    {
        public partial RecipeListModel ToListModel(RecipeDetailModel detailModel);
        
        public partial IList<RecipeListModel> ToListModels(IEnumerable<RecipeDetailModel> detailModels);
    }
}