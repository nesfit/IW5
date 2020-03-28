using AutoMapper;
using CookBook.BL.Api.Models.Recipe;
using CookBook.BL.Common.Facades;
using CookBook.DAL.Entities;
using CookBook.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CookBook.BL.Api.Facades
{
    public class RecipeFacade : IAppFacade
    {
        private readonly RecipeRepository recipeRepository;
        private readonly IngredientAmountRepository ingredientAmountRepository;
        private readonly IngredientRepository ingredientRepository;
        private readonly IMapper mapper;

        public RecipeFacade(
            RecipeRepository recipeRepository,
            IngredientAmountRepository ingredientAmountRepository,
            IngredientRepository ingredientRepository,
            IMapper mapper)
        {
            this.recipeRepository = recipeRepository;
            this.ingredientAmountRepository = ingredientAmountRepository;
            this.ingredientRepository = ingredientRepository;
            this.mapper = mapper;
        }

        public List<RecipeListModel> GetAll()
        {
            var recipeEntities = recipeRepository.GetAll();
            return mapper.Map<List<RecipeListModel>>(recipeEntities);
        }

        public RecipeDetailModel GetById(Guid id)
        {
            var recipeEntity = recipeRepository.GetById(id);
            recipeEntity.IngredientAmounts = ingredientAmountRepository.GetByRecipeId(id);
            foreach (var ingredientAmount in recipeEntity.IngredientAmounts)
            {
                ingredientAmount.Ingredient = ingredientRepository.GetById(ingredientAmount.IngredientId);
            }

            return mapper.Map<RecipeDetailModel>(recipeEntity);
        }

        public Guid Create(RecipeNewModel recipe)
        {
            var recipeEntity = mapper.Map<RecipeEntity>(recipe);
            recipeRepository.Insert(recipeEntity);

            foreach (var ingredientAmount in recipeEntity.IngredientAmounts)
            {
                ingredientAmount.RecipeId = recipeEntity.Id;
                ingredientAmountRepository.Insert(ingredientAmount);
            }

            return recipeEntity.Id;
        }

        public Guid? Update(RecipeUpdateModel recipeUpdateModel)
        {
            var recipeEntityExisting = recipeRepository.GetById(recipeUpdateModel.Id);
            recipeEntityExisting.IngredientAmounts = ingredientAmountRepository.GetByRecipeId(recipeUpdateModel.Id);
            UpdateIngredientAmounts(recipeUpdateModel, recipeEntityExisting);

            var recipeEntityUpdated = mapper.Map<RecipeEntity>(recipeUpdateModel);
            return recipeRepository.Update(recipeEntityUpdated);
        }

        private void UpdateIngredientAmounts(RecipeUpdateModel recipeUpdateModel, RecipeEntity recipeEntity)
        {
            var ingredientAmountsToDelete = recipeEntity.IngredientAmounts.Where(
                ingredientAmount =>
                    !recipeUpdateModel.Ingredients.Any(ingredient => ingredient.IngredientId == ingredientAmount.IngredientId));
            DeleteIngredientAmounts(ingredientAmountsToDelete);

            var recipeUpdateIngredientModelsToInsert = recipeUpdateModel.Ingredients.Where(
                ingredient => !recipeEntity.IngredientAmounts.Any(ingredientAmount => ingredientAmount.IngredientId == ingredient.IngredientId));
            InsertIngredientAmounts(recipeEntity, recipeUpdateIngredientModelsToInsert);

            var recipeUpdateIngredientModelsToUpdate = recipeUpdateModel.Ingredients.Where(
                ingredient => recipeEntity.IngredientAmounts.Any(ingredientAmount => ingredientAmount.IngredientId == ingredient.IngredientId));
            UpdateIngredientAmounts(recipeEntity, recipeUpdateIngredientModelsToUpdate);
        }

        private void UpdateIngredientAmounts(RecipeEntity recipeEntity, IEnumerable<RecipeUpdateIngredientModel> recipeUpdateIngredientModelsToUpdate)
        {
            foreach (var recipeUpdateIngredientModel in recipeUpdateIngredientModelsToUpdate)
            {
                var ingredientAmountEntity =
                    ingredientAmountRepository.GetByRecipeIdAndIngredientId(recipeEntity.Id,
                        recipeUpdateIngredientModel.IngredientId);
                mapper.Map(recipeUpdateIngredientModel, ingredientAmountEntity);
                ingredientAmountRepository.Update(ingredientAmountEntity);
            }
        }

        private void InsertIngredientAmounts(RecipeEntity recipeEntity, IEnumerable<RecipeUpdateIngredientModel> recipeUpdateIngredientModelsToInsert)
        {
            foreach (var recipeUpdateIngredientModel in recipeUpdateIngredientModelsToInsert)
            {
                var ingredientAmountEntity = mapper.Map<IngredientAmountEntity>(recipeUpdateIngredientModel);
                ingredientAmountEntity.RecipeId = recipeEntity.Id;
                ingredientAmountRepository.Insert(ingredientAmountEntity);
            }
        }

        private void DeleteIngredientAmounts(IEnumerable<IngredientAmountEntity> ingredientAmountsToDelete)
        {
            foreach (var ingredientAmountEntity in ingredientAmountsToDelete)
            {
                ingredientAmountRepository.Remove(ingredientAmountEntity.Id);
            }
        }

        public void Delete(Guid id)
        {
            recipeRepository.Remove(id);
        }
    }
}