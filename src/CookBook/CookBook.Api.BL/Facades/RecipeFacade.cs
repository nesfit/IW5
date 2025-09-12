using System;
using System.Collections.Generic;
using System.Linq;
using Mapster;
using CookBook.Api.DAL.Common.Entities;
using CookBook.Api.DAL.Common.Repositories;
using CookBook.Common.Models;

namespace CookBook.Api.BL.Facades
{
    public class RecipeFacade : IRecipeFacade
    {
        private readonly IRecipeRepository recipeRepository;

        public RecipeFacade(
            IRecipeRepository recipeRepository)
        {
            this.recipeRepository = recipeRepository;
        }

        public List<RecipeListModel> GetAll()
        {
            var recipeEntities = recipeRepository.GetAll();
            return recipeEntities.Adapt<List<RecipeListModel>>();
        }

        public RecipeDetailModel? GetById(Guid id)
        {
            var recipeEntity = recipeRepository.GetById(id);
            return recipeEntity?.Adapt<RecipeDetailModel>();
        }

        public Guid CreateOrUpdate(RecipeDetailModel recipeModel)
        {
            return recipeRepository.Exists(recipeModel.Id)
                ? Update(recipeModel)!.Value
                : Create(recipeModel);
        }

        public Guid Create(RecipeDetailModel recipeModel)
        {
            MergeIngredientAmounts(recipeModel);
            var recipeEntity = recipeModel.Adapt<RecipeEntity>();
            return recipeRepository.Insert(recipeEntity);
        }

        public Guid? Update(RecipeDetailModel recipeModel)
        {
            MergeIngredientAmounts(recipeModel);

            var recipeEntity = recipeModel.Adapt<RecipeEntity>();
            recipeEntity.IngredientAmounts = recipeModel.IngredientAmounts.Select(t =>
                new IngredientAmountEntity
                {
                    Id = t.Id,
                    Amount = t.Amount,
                    Unit = t.Unit,
                    RecipeId = recipeEntity.Id,
                    IngredientId = t.Ingredient.Id
                }).ToList();
            var result = recipeRepository.Update(recipeEntity);
            return result;
        }

        public void MergeIngredientAmounts(RecipeDetailModel recipe)
        {
            var result = new List<RecipeDetailIngredientModel>();
            var ingredientAmountGroups = recipe.IngredientAmounts.GroupBy(t => $"{t.Ingredient.Id}-{t.Unit}");

            foreach (var ingredientAmountGroup in ingredientAmountGroups)
            {
                var ingredientAmountFirst = ingredientAmountGroup.First();
                var totalAmount = ingredientAmountGroup.Sum(t => t.Amount);
                var ingredientAmount = ingredientAmountFirst with { Amount = totalAmount };

                result.Add(ingredientAmount);
            }

            recipe.IngredientAmounts = result;
        }

        public void Delete(Guid id)
        {
            recipeRepository.Remove(id);
        }
    }
}
