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
        private readonly IMapper mapper;

        public RecipeFacade(
            IRecipeRepository recipeRepository,
            IIngredientAmountRepository ingredientAmountRepository,
            IIngredientRepository ingredientRepository,
            IMapper mapper)
        {
            this.recipeRepository = recipeRepository;
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
            return mapper.Map<RecipeDetailModel>(recipeEntity);
        }

        public Guid CreateOrUpdate(RecipeDetailModel recipeModel)
        {
            return recipeRepository.Exists(recipeModel.Id)
                ? Update(recipeModel)!.Value
                : Create(recipeModel);
        }

        public Guid Create(RecipeDetailModel recipeModel)
        {
            MergeIngredients(recipeModel);
            var recipeEntity = mapper.Map<RecipeEntity>(recipeModel);
            var result = recipeRepository.Insert(recipeEntity);
            return result;
        }

        private void MergeIngredients(RecipeDetailModel recipe)
        {
            var result = new List<RecipeDetailIngredientModel>();
            var ingredientGroups = recipe.IngredientAmounts.GroupBy(t => $"{t.Ingredient.Id}-{t.Unit}");

            foreach (var group in ingredientGroups)
            {
                var representative = @group.First();
                var totalAmount = group.Sum(t => t.Amount);
                var ingredient =
                    new RecipeDetailIngredientModel(totalAmount, representative.Unit, representative.Ingredient);
                result.Add(ingredient);
            }

            recipe.IngredientAmounts = result;
        }

        public Guid? Update(RecipeDetailModel recipeModel)
        {
            MergeIngredients(recipeModel);

            var recipeEntity = mapper.Map<RecipeEntity>(recipeModel);
            recipeEntity.IngredientAmounts = recipeModel.IngredientAmounts.Select(t =>
                new IngredientAmountEntity(t.Amount, t.Unit, recipeEntity.Id, t.Ingredient.Id)).ToList();
            var result = recipeRepository.Update(recipeEntity);
            return result;
        }

        public void Delete(Guid id)
        {
            recipeRepository.Remove(id);
        }
    }
}
