using CookBook.Common.Models;
using Riok.Mapperly.Abstractions;

namespace CookBook.Web.BL.Mappers
{
    [Mapper]
    public partial class RecipeMapper : IMapper<RecipeDetailModel, RecipeListModel>
    {
        [MapperIgnoreSource(nameof(RecipeDetailModel.Description))]
        [MapperIgnoreSource(nameof(RecipeDetailModel.IngredientAmounts))]
        public partial RecipeListModel ToListModel(RecipeDetailModel detailModel);
        
        public partial IList<RecipeListModel> ToListModels(IEnumerable<RecipeDetailModel> detailModels);
    }
}
