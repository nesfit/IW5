using AutoMapper;
using CookBook.BL.Common.Facades;
using CookBook.DAL.Entities;
using CookBook.DAL.Repositories;
using CookBook.Models;
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

        public Guid Create(RecipeDetailModel recipe)
        {
            var recipeEntity = mapper.Map<RecipeEntity>(recipe);
            recipeEntity.Id = Guid.NewGuid();
            recipeRepository.Insert(recipeEntity);

            foreach (var ingredientAmount in recipeEntity.IngredientAmounts)
            {
                ingredientAmount.RecipeId = recipeEntity.Id;
                ingredientAmountRepository.Insert(ingredientAmount);
            }

            return recipeEntity.Id;
        }

        public Guid? Update(RecipeDetailModel recipe)
        {
            var recipeEntityExisting = recipeRepository.GetById(recipe.Id);
            recipeEntityExisting.IngredientAmounts = ingredientAmountRepository.GetByRecipeId(recipe.Id);
            UpdateIngredientAmounts(recipe, recipeEntityExisting);

            var recipeEntityUpdated = mapper.Map<RecipeEntity>(recipe);
            return recipeRepository.Update(recipeEntityUpdated);
        }

        private void UpdateIngredientAmounts(RecipeDetailModel recipeModel, RecipeEntity recipeEntity)
        {
            var ingredientAmountsToDelete = recipeEntity.IngredientAmounts.Where(
                ingredientAmount =>
                    !recipeModel.Ingredients.Any(ingredient => ingredient.Ingredient.Id == ingredientAmount.IngredientId));
            DeleteIngredientAmounts(ingredientAmountsToDelete);

            var recipeUpdateIngredientModelsToInsert = recipeModel.Ingredients.Where(
                ingredient => !recipeEntity.IngredientAmounts.Any(ingredientAmount => ingredientAmount.IngredientId == ingredient.Ingredient.Id));
            InsertIngredientAmounts(recipeEntity, recipeUpdateIngredientModelsToInsert);

            var recipeUpdateIngredientModelsToUpdate = recipeModel.Ingredients.Where(
                ingredient => recipeEntity.IngredientAmounts.Any(ingredientAmount => ingredientAmount.IngredientId == ingredient.Ingredient.Id));
            UpdateIngredientAmounts(recipeEntity, recipeUpdateIngredientModelsToUpdate);
        }

        private void UpdateIngredientAmounts(RecipeEntity recipeEntity, IEnumerable<RecipeListIngredientModel> recipeUpdateIngredientModelsToUpdate)
        {
            foreach (var recipeUpdateIngredientModel in recipeUpdateIngredientModelsToUpdate)
            {
                var ingredientAmountEntity =
                    ingredientAmountRepository.GetByRecipeIdAndIngredientId(recipeEntity.Id,
                        recipeUpdateIngredientModel.Ingredient.Id);
                mapper.Map(recipeUpdateIngredientModel, ingredientAmountEntity);
                ingredientAmountRepository.Update(ingredientAmountEntity);
            }
        }

        private void InsertIngredientAmounts(RecipeEntity recipeEntity, IEnumerable<RecipeListIngredientModel> recipeUpdateIngredientModelsToInsert)
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