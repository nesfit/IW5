using CookBook.Api.DAL.Common.Entities;
using Riok.Mapperly.Abstractions;

namespace CookBook.Api.DAL.Common.Mappers
{
    [Mapper]
    public partial class RecipeMapper
    {
        [MapperIgnoreSource(nameof(RecipeEntity.Id))]
        [MapperIgnoreSource(nameof(RecipeEntity.OwnerId))]
        [MapperIgnoreTarget(nameof(RecipeEntity.OwnerId))]
        public partial void UpdateEntity(RecipeEntity source, RecipeEntity destination);
    }
}
