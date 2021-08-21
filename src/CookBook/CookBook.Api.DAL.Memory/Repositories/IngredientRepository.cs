using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CookBook.Api.DAL.Common.Entities;
using CookBook.Api.DAL.Common.Repositories;

namespace CookBook.Api.DAL.Memory.Repositories
{
    public class IngredientRepository : IIngredientRepository
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

        public IngredientEntity? GetById(Guid id)
        {
            return ingredients.SingleOrDefault(entity => entity.Id == id);
        }

        public Guid Insert(IngredientEntity ingredient)
        {
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

        public bool Exists(Guid id)
        {
            return ingredients.Any(ingredient => ingredient.Id == id);
        }
    }
}
