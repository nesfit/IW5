using CookBook.Api.DAL.Common.Entities;
using CookBook.Common.Models;
using Riok.Mapperly.Abstractions;

namespace CookBook.Api.BL.Mappers
{
    [Mapper]
    public partial class RecipeMapper
    {
        [MapperIgnoreSource(nameof(RecipeEntity.Description))]
        [MapperIgnoreSource(nameof(RecipeEntity.IngredientAmounts))]
        public partial RecipeListModel ToListModel(RecipeEntity entity);

        [MapProperty(nameof(RecipeEntity.IngredientAmounts), nameof(RecipeDetailModel.IngredientAmounts))]
        public partial RecipeDetailModel ToDetailModel(RecipeEntity entity);

        [MapperIgnoreTarget(nameof(RecipeEntity.IngredientAmounts))]
        [MapperIgnoreSource(nameof(RecipeDetailModel.IngredientAmounts))]
        public partial RecipeEntity ToEntity(RecipeDetailModel model);

        [MapperIgnoreSource(nameof(IngredientAmountEntity.RecipeId))]
        [MapperIgnoreSource(nameof(IngredientAmountEntity.Recipe))]
        [MapperIgnoreSource(nameof(IngredientAmountEntity.IngredientId))]
        [MapProperty(nameof(IngredientAmountEntity.Ingredient), nameof(RecipeDetailIngredientModel.Ingredient))]
        public partial RecipeDetailIngredientModel ToRecipeDetailIngredientModel(IngredientAmountEntity entity);

        [MapperIgnoreSource(nameof(IngredientEntity.Description))]
        public partial IngredientListModel ToIngredientListModel(IngredientEntity entity);

        public partial List<RecipeListModel> ToListModels(IEnumerable<RecipeEntity> entities);
    }
}
