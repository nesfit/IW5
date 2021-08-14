using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CookBook.Api.DAL.Entities;

namespace CookBook.Api.DAL.Repositories
{
    public class IngredientAmountRepository : IIngredientAmountRepository
    {
        private readonly IList<IngredientAmountEntity> ingredientAmounts;
        private readonly IMapper mapper;

        public IngredientAmountRepository(
            Storage storage,
            IMapper mapper)
        {
            ingredientAmounts = storage.IngredientAmounts;
            this.mapper = mapper;
        }

        public IList<IngredientAmountEntity> GetAll()
        {
            return ingredientAmounts;
        }

        public IList<IngredientAmountEntity> GetByRecipeId(Guid recipeId)
        {
            return ingredientAmounts.Where(ingredientAmount => ingredientAmount.RecipeId == recipeId).ToList();
        }

        public IngredientAmountEntity? GetByRecipeIdAndIngredientId(Guid recipeId, Guid ingredientId)
        {
            return ingredientAmounts.SingleOrDefault(ingredientAmount => ingredientAmount.RecipeId == recipeId && ingredientAmount.IngredientId == ingredientId);
        }

        public IngredientAmountEntity? GetById(Guid id)
        {
            return ingredientAmounts.SingleOrDefault(ingredientAmount => ingredientAmount.Id == id);
        }

        public Guid Insert(IngredientAmountEntity ingredientAmount)
        {
            ingredientAmounts.Add(ingredientAmount);
            return ingredientAmount.Id;
        }

        public Guid? Update(IngredientAmountEntity ingredientAmoungUpdated)
        {
            var ingredientAmountExisting = ingredientAmounts.SingleOrDefault(ingredientAmountInStorage => ingredientAmountInStorage.Id == ingredientAmoungUpdated.Id);
            if (ingredientAmountExisting != null)
            {
                mapper.Map(ingredientAmoungUpdated, ingredientAmountExisting);
            }

            return ingredientAmountExisting?.Id;
        }

        public void Remove(Guid id)
        {
            var ingredientAmountToRemove = ingredientAmounts.Single(ingredientAmount => ingredientAmount.Id.Equals(id));
            ingredientAmounts.Remove(ingredientAmountToRemove);
        }
    }
}
