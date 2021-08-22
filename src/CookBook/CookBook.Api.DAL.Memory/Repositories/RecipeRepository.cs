using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CookBook.Api.DAL.Common.Entities;
using CookBook.Api.DAL.Common.Repositories;

namespace CookBook.Api.DAL.Memory.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly IList<RecipeEntity> recipes;
        private readonly IList<IngredientAmountEntity> ingredientAmounts;
        private readonly IList<IngredientEntity> ingredients;

        public RecipeRepository(
            Storage storage)
        {
            this.recipes = storage.Recipes;
            this.ingredientAmounts = storage.IngredientAmounts;
            this.ingredients = storage.Ingredients;

        }

        public IList<RecipeEntity> GetAll()
        {
            return this.recipes;
        }

        public RecipeEntity? GetById(Guid id)
        {
            var recipeEntity = recipes.SingleOrDefault(recipe => recipe.Id == id);

            if (recipeEntity is not null)
            {
                recipeEntity.IngredientAmounts = GetIngredientAmountsByRecipeId(id);
                foreach (var ingredientAmount in recipeEntity.IngredientAmounts)
                {
                    ingredientAmount.Ingredient = ingredients.SingleOrDefault(ingredientEntity => ingredientEntity.Id == ingredientAmount.IngredientId);
                }
            }

            return recipeEntity;
        }

        public Guid Insert(RecipeEntity entity)
        {
            recipes.Add(entity);

            foreach (var ingredientAmount in entity.IngredientAmounts)
            {
                var ingredientAmountEntity = new IngredientAmountEntity(ingredientAmount.Amount, ingredientAmount.Unit,
                    entity.Id, ingredientAmount.IngredientId);
                ingredientAmounts.Add(ingredientAmountEntity);
            }

            return entity.Id;
        }

        public Guid? Update(RecipeEntity entity)
        {
            var recipeEntityExisting = recipes.SingleOrDefault(recipeEntity => recipeEntity.Id == entity.Id);

            if (recipeEntityExisting is not null)
            {
                recipeEntityExisting.IngredientAmounts = GetIngredientAmountsByRecipeId(entity.Id);
                UpdateIngredientAmounts(entity, recipeEntityExisting);
                return recipeEntityExisting.Id;
            }
            else
            {
                return null;
            }
        }

        private void UpdateIngredientAmounts(RecipeEntity updatedEntity, RecipeEntity existingEntity)
        {
            var ingredientAmountsToDelete = existingEntity.IngredientAmounts.Where(
                    ingredientAmount => updatedEntity.IngredientAmounts.All(ia => ia.IngredientId != ingredientAmount.IngredientId))
                    .ToList();
            DeleteIngredientAmounts(ingredientAmountsToDelete);

            var recipeUpdateIngredientModelsToInsert = updatedEntity.IngredientAmounts.Where(
                ingredient => existingEntity.IngredientAmounts.All(ia => ia.IngredientId != ingredient.IngredientId));
            InsertIngredientAmounts(existingEntity, recipeUpdateIngredientModelsToInsert);

            var recipeUpdateIngredientModelsToUpdate = updatedEntity.IngredientAmounts.Where(
                ingredient => existingEntity.IngredientAmounts.Any(ia => ia.IngredientId == ingredient.IngredientId));
            UpdateIngredientAmounts(existingEntity, recipeUpdateIngredientModelsToUpdate);
        }

        private void UpdateIngredientAmounts(RecipeEntity recipeEntity,
            IEnumerable<IngredientAmountEntity> recipeIngredientModelsToUpdate)
        {
            foreach (var recipeUpdateIngredientModel in recipeIngredientModelsToUpdate)
            {
                var ingredientAmountEntity =
                    GetIngredientAmountRecipeIdAndIngredientId(recipeEntity.Id,
                        recipeUpdateIngredientModel.IngredientId);

                if (ingredientAmountEntity is not null)
                {
                    ingredientAmountEntity.Amount = recipeUpdateIngredientModel.Amount;
                    ingredientAmountEntity.Unit = recipeUpdateIngredientModel.Unit;
                    ingredientAmountEntity.IngredientId = recipeUpdateIngredientModel.IngredientId;
                }
            }
        }

        private void DeleteIngredientAmounts(IList<IngredientAmountEntity> ingredientAmountsToDelete)
        {
            for (int i = 0; i < ingredientAmountsToDelete.Count; i++)
            {
                var ingredientAmountEntity = ingredientAmountsToDelete.ElementAt(i);
                ingredientAmounts.Remove(ingredientAmountEntity);
            }
        }

        private void InsertIngredientAmounts(RecipeEntity existingEntity,
            IEnumerable<IngredientAmountEntity> recipeIngredientModelsToInsert)
        {
            foreach (var ingredientModel in recipeIngredientModelsToInsert)
            {
                var ingredientAmountEntity = new IngredientAmountEntity(ingredientModel.Amount, ingredientModel.Unit,
                    existingEntity.Id, ingredientModel.IngredientId) { RecipeId = existingEntity.Id };

                ingredientAmounts.Add(ingredientAmountEntity);
            }
        }

        private IList<IngredientAmountEntity> GetIngredientAmountsByRecipeId(Guid recipeId)
        {
            return ingredientAmounts.Where(ingredientAmountEntity => ingredientAmountEntity.RecipeId == recipeId).ToList();
        }

        private IngredientAmountEntity? GetIngredientAmountRecipeIdAndIngredientId(Guid recipeId, Guid ingredientId)
        {
            return ingredientAmounts.SingleOrDefault(entity => entity.RecipeId == recipeId && entity.IngredientId == ingredientId);
        }

        public void Remove(Guid id)
        {
            var ingredientAmountsToRemove = ingredientAmounts.Where(ingredientAmount => ingredientAmount.RecipeId == id).ToList();

            for (var i = 0; i < ingredientAmountsToRemove.Count; i++)
            {
                var ingredientAmountToRemove = ingredientAmountsToRemove.ElementAt(i);
                ingredientAmounts.Remove(ingredientAmountToRemove);
            }
            
            var recipeToRemove = recipes.SingleOrDefault(recipeEntity => recipeEntity.Id == id);
            if (recipeToRemove is not null)
            {
                recipes.Remove(recipeToRemove);
            }
        }

        public bool Exists(Guid id)
        {
            return recipes.Any(recipe => recipe.Id == id);
        }
    }
}
