using CookBook.Api.DAL.Common.Entities;
using Riok.Mapperly.Abstractions;

namespace CookBook.Api.DAL.Common.Mappers
{
    [Mapper]
    public partial class IngredientMapper
    {
        [MapperIgnoreSource(nameof(IngredientEntity.Id))]
        [MapperIgnoreSource(nameof(IngredientEntity.OwnerId))]
        [MapperIgnoreTarget(nameof(IngredientEntity.OwnerId))]
        public partial void UpdateEntity(IngredientEntity source, IngredientEntity destination);
    }
}
