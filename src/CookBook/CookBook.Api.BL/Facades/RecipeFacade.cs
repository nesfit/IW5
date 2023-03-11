using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CookBook.Api.DAL.Common.Entities;
using CookBook.Api.DAL.Common.Repositories;
using CookBook.Common.Models;

namespace CookBook.Api.BL.Facades
{
    public class RecipeFacade : IRecipeFacade
    {
        private readonly IRecipeRepository recipeRepository;
        private readonly IIngredientAmountRepository ingredientAmountRepository;
        private readonly IMapper mapper;

        public RecipeFacade(
            IRecipeRepository recipeRepository,
            IIngredientAmountRepository ingredientAmountRepository,
            IMapper mapper)
        {
            this.recipeRepository = recipeRepository;
            this.ingredientAmountRepository = ingredientAmountRepository;
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
            var recipeEntity = mapper.Map<RecipeEntity>(recipeModel);
            var result = recipeRepository.Insert(recipeEntity);
            CreateOrUpdateIngredientAmounts(result, recipeModel.IngredientAmounts);
            
            return result;
        }

        public Guid? Update(RecipeDetailModel recipeModel)
        {
            var recipeEntity = mapper.Map<RecipeEntity>(recipeModel);
            var result = recipeRepository.Update(recipeEntity);
            if (result is not null)
            {
                CreateOrUpdateIngredientAmounts(result.Value, recipeModel.IngredientAmounts);
            }

            return result;
        }

        private void CreateOrUpdateIngredientAmounts(Guid recipeId, IEnumerable<RecipeDetailIngredientModel> ingredientAmountModels)
        {
            foreach (var ingredientAmountModel in MergeIngredientAmounts(ingredientAmountModels))
            {
                var existingIngredientAmountEntity = ingredientAmountRepository.GetById(ingredientAmountModel.Id);

                if (existingIngredientAmountEntity is null)
                {
                    var ingredientAmountEntity = mapper.Map<IngredientAmountEntity>(ingredientAmountModel);
                    ingredientAmountEntity.RecipeId = recipeId;
                    ingredientAmountRepository.Insert(ingredientAmountEntity);
                }
                else
                {
                    var ingredientAmountEntity = mapper.Map<IngredientAmountEntity>(ingredientAmountModel);
                    ingredientAmountEntity.RecipeId = recipeId;
                    ingredientAmountRepository.Update(ingredientAmountEntity);
                }
            }
        }

        public IList<RecipeDetailIngredientModel> MergeIngredientAmounts(IEnumerable<RecipeDetailIngredientModel> ingredientAmountModels)
        {
            var result = new List<RecipeDetailIngredientModel>();
            var ingredientAmountGroups = ingredientAmountModels.GroupBy(t => $"{t.Ingredient.Id}-{t.Unit}");

            foreach (var ingredientAmountGroup in ingredientAmountGroups)
            {
                var ingredientAmountFirst = ingredientAmountGroup.First();
                var totalAmount = ingredientAmountGroup.Sum(t => t.Amount);
                var ingredientAmount = ingredientAmountFirst with { Amount = totalAmount };

                result.Add(ingredientAmount);
            }

            return result;
        }

        public void Delete(Guid id)
        {
            recipeRepository.Remove(id);
        }
    }
}
