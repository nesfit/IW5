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
            MergeIngredientAmounts(recipeModel);
            var recipeEntity = mapper.Map<RecipeEntity>(recipeModel);
            return recipeRepository.Insert(recipeEntity);
        }

        public Guid? Update(RecipeDetailModel recipeModel)
        {
            MergeIngredientAmounts(recipeModel);

            var recipeEntity = mapper.Map<RecipeEntity>(recipeModel);
            recipeEntity.IngredientAmounts = recipeModel.IngredientAmounts.Select(t =>
                new IngredientAmountEntity(t.Id, t.Amount, t.Unit, recipeEntity.Id, t.Ingredient.Id)).ToList();
            var result = recipeRepository.Update(recipeEntity);
            return result;
        }

        private static void MergeIngredientAmounts(RecipeDetailModel recipe)
        {
            var result = new List<RecipeDetailIngredientModel>();
            var ingredientAmountGroups = recipe.IngredientAmounts.GroupBy(t => t.Ingredient.Id);

            foreach (var ingredientAmountGroup in ingredientAmountGroups)
            {
                var ingredientAmountFirst = ingredientAmountGroup.First();
                var totalAmount = ingredientAmountGroup.Sum(t => t.Amount);
                var ingredientAmount = new RecipeDetailIngredientModel(ingredientAmountFirst.Id, totalAmount, ingredientAmountFirst.Unit, ingredientAmountFirst.Ingredient);
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
