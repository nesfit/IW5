using CookBook.Api.DAL.Common.Entities;
using Riok.Mapperly.Abstractions;

namespace CookBook.Api.DAL.Common.Mappers
{
    [Mapper]
    public partial class IngredientMapper
    {
        [MapperIgnoreSource(nameof(IngredientEntity.Id))]
        public partial void UpdateEntity(IngredientEntity source, IngredientEntity destination);
    }
}