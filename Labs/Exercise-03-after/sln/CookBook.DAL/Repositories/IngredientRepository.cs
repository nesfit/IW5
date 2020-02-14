using AutoMapper;
using CookBook.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CookBook.DAL.Repositories
{
    public class IngredientRepository : IAppRepository<IngredientEntity>
    {
        private readonly IList<IngredientEntity> ingredients;
        private readonly IMapper mapper;

        public IngredientRepository(
            Storage storage,
            IMapper mapper)
        {
            ingredients = storage.Ingredients;
            this.mapper = mapper;
        }

        public IList<IngredientEntity> GetAll()
        {
            return ingredients;
        }

        public IngredientEntity GetById(Guid id)
        {
            return ingredients.SingleOrDefault(entity => entity.Id == id);
        }

        public Guid Insert(IngredientEntity ingredient)
        {
            ingredient.Id = Guid.NewGuid();
            ingredients.Add(ingredient);
            return ingredient.Id;
        }

        public Guid? Update(IngredientEntity ingredientUpdated)
        {
            var ingredientExisting = ingredients.SingleOrDefault(ingredientInStorage => ingredientInStorage.Id == ingredientUpdated.Id);
            if (ingredientExisting != null)
            {
                mapper.Map(ingredientUpdated, ingredientExisting);
            }

            return ingredientExisting?.Id;
        }

        public void Remove(Guid id)
        {
            var ingredientToRemove = ingredients.Single(ingredient => ingredient.Id.Equals(id));
            ingredients.Remove(ingredientToRemove);
        }
    }
}