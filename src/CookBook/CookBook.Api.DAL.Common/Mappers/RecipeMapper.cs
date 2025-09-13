using CookBook.Api.DAL.Common.Entities;
using Riok.Mapperly.Abstractions;

namespace CookBook.Api.DAL.Common.Mappers
{
    [Mapper]
    public partial class RecipeMapper
    {
        [MapperIgnoreSource(nameof(RecipeEntity.Id))]
        public partial void UpdateEntity(RecipeEntity source, RecipeEntity destination);
    }
}
