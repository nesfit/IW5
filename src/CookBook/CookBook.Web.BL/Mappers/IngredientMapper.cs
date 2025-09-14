using CookBook.Common.Models;
using Riok.Mapperly.Abstractions;

namespace CookBook.Web.BL.Mappers
{
    [Mapper]
    public partial class IngredientMapper : IMapper<IngredientDetailModel, IngredientListModel>
    {
        [MapperIgnoreSource(nameof(IngredientDetailModel.Description))]
        public partial IngredientListModel ToListModel(IngredientDetailModel detailModel);
        
        public partial IList<IngredientListModel> ToListModels(IEnumerable<IngredientDetailModel> detailModels);
    }
}
