using System;
using System.Collections.Generic;
using System.Linq;
using CookBook.Api.DAL.Common.Entities;
using CookBook.Api.DAL.Common.Repositories;

namespace CookBook.Api.DAL.Memory.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly RecipeCoreRepository recipeRepository;
        private readonly IIngredientAmountRepository ingredientAmountRepository;
        private readonly IIngredientRepository ingredientRepository;

        public RecipeRepository(RecipeCoreRepository recipeRepository,
            IIngredientAmountRepository ingredientAmountRepository, IIngredientRepository ingredientRepository)
        {
            this.recipeRepository = recipeRepository;
            this.ingredientAmountRepository = ingredientAmountRepository;
            this.ingredientRepository = ingredientRepository;
        }

        public IList<RecipeEntity> GetAll()
        {
            return recipeRepository.GetAll();
        }

        public RecipeEntity? GetById(Guid id)
        {
            var recipeEntity = recipeRepository.GetById(id);
            if (recipeEntity is not null)
            {
                recipeEntity.IngredientAmounts = ingredientAmountRepository.GetByRecipeId(id);
                foreach (var ingredientAmount in recipeEntity.IngredientAmounts)
                {
                    ingredientAmount.Ingredient = ingredientRepository.GetById(ingredientAmount.IngredientId);
                }
            }

            return recipeEntity;
        }

        public Guid Insert(RecipeEntity entity)
        {
            recipeRepository.Insert(entity);

            foreach (var ingredientAmount in entity.IngredientAmounts)
            {
                var ingredientAmountEntity = new IngredientAmountEntity(ingredientAmount.Amount, ingredientAmount.Unit,
                    entity.Id, ingredientAmount.Ingredient!.Id);
                ingredientAmountRepository.Insert(ingredientAmountEntity);
            }

            return entity.Id;
        }

        public Guid? Update(RecipeEntity entity)
        {
            var recipeEntityExisting = recipeRepository.GetById(entity.Id);

            if (recipeEntityExisting is not null)
            {
                recipeEntityExisting.IngredientAmounts =
                    ingredientAmountRepository.GetByRecipeId(entity.Id);
                UpdateIngredientAmounts(entity, recipeEntityExisting);

                return recipeRepository.Update(recipeEntityExisting);
            }
            else
            {
                return null;
            }
        }


        private void UpdateIngredientAmounts(RecipeEntity updatedEntity, RecipeEntity existingEntity)
        {
            var ingredientAmountsToDelete = existingEntity.IngredientAmounts.Where(
                ingredientAmount =>
                    updatedEntity.IngredientAmounts.All(ia => ia.IngredientId != ingredientAmount.IngredientId));
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
                    ingredientAmountRepository.GetByRecipeIdAndIngredientId(recipeEntity.Id,
                        recipeUpdateIngredientModel.IngredientId);

                if (ingredientAmountEntity is not null)
                {
                    ingredientAmountEntity.Amount = recipeUpdateIngredientModel.Amount;
                    ingredientAmountEntity.Unit = recipeUpdateIngredientModel.Unit;
                    ingredientAmountEntity.IngredientId = recipeUpdateIngredientModel.IngredientId;

                    ingredientAmountRepository.Update(ingredientAmountEntity);
                }
            }
        }

        private void DeleteIngredientAmounts(IEnumerable<IngredientAmountEntity> ingredientAmountsToDelete)
        {
            var ingredients = ingredientAmountsToDelete as IngredientAmountEntity[] ??
                              ingredientAmountsToDelete.ToArray();
            for (int i = 0; i < ingredients.Count(); i++)
            {
                var ingredientAmountEntity = ingredients.ElementAt(i);
                ingredientAmountRepository.Remove(ingredientAmountEntity.Id);
            }
        }

        private void InsertIngredientAmounts(RecipeEntity existingEntity,
            IEnumerable<IngredientAmountEntity> recipeIngredientModelsToInsert)
        {
            foreach (var ingredientModel in recipeIngredientModelsToInsert)
            {
                var ingredientAmountEntity = new IngredientAmountEntity(ingredientModel.Amount, ingredientModel.Unit,
                    existingEntity.Id, ingredientModel.IngredientId) { RecipeId = existingEntity.Id };
                ingredientAmountRepository.Insert(ingredientAmountEntity);
            }
        }


        public void Remove(Guid id)
        {
            recipeRepository.Remove(id);
        }

        public bool Exists(Guid id)
        {
            return recipeRepository.Exists(id);
        }
    }
}
