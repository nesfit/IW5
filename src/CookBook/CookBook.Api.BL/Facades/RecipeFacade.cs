using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CookBook.Api.DAL.Common.Entities;
using CookBook.Api.DAL.Common.Repositories;
using CookBook.Common.BL.Facades;
using CookBook.Common.Models;

namespace CookBook.Api.BL.Facades
{
    public class RecipeFacade : IAppFacade
    {
        private readonly IRecipeRepository recipeRepository;
        private readonly IIngredientAmountRepository ingredientAmountRepository;
        private readonly IIngredientRepository ingredientRepository;
        private readonly IMapper mapper;

        public RecipeFacade(
            IRecipeRepository recipeRepository,
            IIngredientAmountRepository ingredientAmountRepository,
            IIngredientRepository ingredientRepository,
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

        public RecipeDetailModel? GetById(Guid id)
        {
            var recipeEntity = recipeRepository.GetById(id);
            if(recipeEntity is not null)
            {
                recipeEntity.IngredientAmounts = ingredientAmountRepository.GetByRecipeId(id);
                foreach (var ingredientAmount in recipeEntity.IngredientAmounts)
                {
                    ingredientAmount.Ingredient = ingredientRepository.GetById(ingredientAmount.IngredientId);
                }
            }

            return mapper.Map<RecipeDetailModel>(recipeEntity);
        }

        public Guid CreateOrUpdate(RecipeDetailModel recipeModel)
        {
            var existingEntity = recipeRepository.GetById(recipeModel.Id);
            return existingEntity is null
                ? Create(recipeModel)
                : Update(recipeModel)!.Value;
        }

        public Guid Create(RecipeDetailModel recipeModel)
        {
            var recipeEntity = mapper.Map<RecipeEntity>(recipeModel);
            recipeRepository.Insert(recipeEntity);

            foreach (var ingredientAmountModel in recipeModel.IngredientAmounts)
            {
                var ingredientAmountEntity = new IngredientAmountEntity(ingredientAmountModel.Amount, ingredientAmountModel.Unit,
                    recipeEntity.Id, ingredientAmountModel.Ingredient.Id);
                ingredientAmountRepository.Insert(ingredientAmountEntity);
            }

            return recipeEntity.Id;
        }

        public Guid? Update(RecipeDetailModel recipeModel)
        {
            var recipeEntityExisting = recipeRepository.GetById(recipeModel.Id);
            if(recipeEntityExisting is not null)
            {
                recipeEntityExisting.IngredientAmounts = ingredientAmountRepository.GetByRecipeId(recipeModel.Id);
                UpdateIngredientAmounts(recipeModel, recipeEntityExisting);

                var recipeEntityUpdated = mapper.Map<RecipeEntity>(recipeModel);
                return recipeRepository.Update(recipeEntityUpdated);
            }
            else
            {
                return null;
            }
        }

        private void UpdateIngredientAmounts(RecipeDetailModel recipeModel, RecipeEntity recipeEntity)
        {
            var ingredientAmountsToDelete = recipeEntity.IngredientAmounts.Where(
                ingredientAmount =>
                    !recipeModel.IngredientAmounts.Any(ia => ia.Ingredient.Id == ingredientAmount.IngredientId));
            DeleteIngredientAmounts(ingredientAmountsToDelete);

            var recipeUpdateIngredientModelsToInsert = recipeModel.IngredientAmounts.Where(
                ingredient => !recipeEntity.IngredientAmounts.Any(ia => ia.IngredientId == ingredient.Ingredient.Id));
            InsertIngredientAmounts(recipeEntity, recipeUpdateIngredientModelsToInsert);

            var recipeUpdateIngredientModelsToUpdate = recipeModel.IngredientAmounts.Where(
                ingredient => recipeEntity.IngredientAmounts.Any(ia => ia.IngredientId == ingredient.Ingredient.Id));
            UpdateIngredientAmounts(recipeEntity, recipeUpdateIngredientModelsToUpdate);
        }

        private void UpdateIngredientAmounts(RecipeEntity recipeEntity, IEnumerable<RecipeDetailIngredientModel> recipeIngredientModelsToUpdate)
        {
            foreach (var recipeUpdateIngredientModel in recipeIngredientModelsToUpdate)
            {
                var ingredientAmountEntity =
                    ingredientAmountRepository.GetByRecipeIdAndIngredientId(recipeEntity.Id,
                        recipeUpdateIngredientModel.Ingredient.Id);

                if(ingredientAmountEntity is not null)
                {
                    ingredientAmountEntity.Amount = recipeUpdateIngredientModel.Amount;
                    ingredientAmountEntity.Unit = recipeUpdateIngredientModel.Unit;
                    ingredientAmountEntity.IngredientId = recipeUpdateIngredientModel.Ingredient.Id;

                    ingredientAmountRepository.Update(ingredientAmountEntity);
                }
            }
        }

        private void InsertIngredientAmounts(RecipeEntity recipeEntity, IEnumerable<RecipeDetailIngredientModel> recipeIngredientModelsToInsert)
        {
            foreach (var ingredientModel in recipeIngredientModelsToInsert)
            {
                var ingredientAmountEntity = new IngredientAmountEntity(ingredientModel.Amount, ingredientModel.Unit,
                    recipeEntity.Id, ingredientModel.Ingredient.Id);
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
