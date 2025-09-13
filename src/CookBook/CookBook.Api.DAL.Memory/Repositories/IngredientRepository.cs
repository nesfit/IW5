using CookBook.Api.DAL.Common.Entities;
using CookBook.Api.DAL.Common.Mappers;
using CookBook.Api.DAL.Common.Repositories;

namespace CookBook.Api.DAL.Memory.Repositories
{
    public class IngredientRepository(
        IngredientMapper ingredientMapper,
        Storage storage)
        : IIngredientRepository
    {
        private readonly IList<IngredientEntity> ingredients = storage.Ingredients;
        private readonly IList<IngredientAmountEntity> ingredientAmounts = storage.IngredientAmounts;

        public IList<IngredientEntity> GetAll()
        {
            return ingredients;
        }

        public IngredientEntity? GetById(Guid id)
        {
            return ingredients.SingleOrDefault(entity => entity.Id == id);
        }

        public Guid Insert(IngredientEntity ingredient)
        {
            ingredients.Add(ingredient);
            return ingredient.Id;
        }

        public Guid? Update(IngredientEntity entity)
        {
            var ingredientExisting = ingredients.SingleOrDefault(ingredientInStorage =>
                ingredientInStorage.Id == entity.Id);

            if (ingredientExisting is not null)
            {
                ingredientMapper.UpdateEntity(entity, ingredientExisting);
                return ingredientExisting.Id;
            }

            return null;
        }

        public void Remove(Guid id)
        {
            var ingredientAmountsToRemove =
                ingredientAmounts.Where(ingredientAmount => ingredientAmount.IngredientId == id).ToList();

            for (var i = 0; i < ingredientAmountsToRemove.Count; i++)
            {
                var ingredientAmountToRemove = ingredientAmountsToRemove.ElementAt(i);
                ingredientAmounts.Remove(ingredientAmountToRemove);
            }

            var ingredientToRemove = ingredients.Single(ingredient => ingredient.Id.Equals(id));
            ingredients.Remove(ingredientToRemove);
        }

        public bool Exists(Guid id)
        {
            return ingredients.Any(ingredient => ingredient.Id == id);
        }
    }
}
