using CookBook.Api.DAL.Common.Entities;
using CookBook.Common.Models;
using Riok.Mapperly.Abstractions;

namespace CookBook.Api.BL.Mappers
{
    [Mapper]
    public partial class IngredientMapper
    {
        [MapperIgnoreSource(nameof(IngredientEntity.Description))]
        public partial IngredientListModel ToListModel(IngredientEntity entity);
        
        public partial IngredientDetailModel ToDetailModel(IngredientEntity entity);
        public partial IngredientEntity ToEntity(IngredientDetailModel model);
        
        public partial List<IngredientListModel> ToListModels(IEnumerable<IngredientEntity> entities);
    }
}