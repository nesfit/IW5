using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CookBook.Api.DAL.Entities;

namespace CookBook.Api.DAL.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly IList<RecipeEntity> recipes;
        private readonly IMapper mapper;

        public RecipeRepository(
            Storage storage,
            IMapper mapper)
        {
            this.recipes = storage.Recipes;
            this.mapper = mapper;
        }

        public IList<RecipeEntity> GetAll()
        {
            return recipes;
        }

        public RecipeEntity? GetById(Guid id)
        {
            return recipes.SingleOrDefault(recipe => recipe.Id == id);
        }

        public Guid Insert(RecipeEntity recipe)
        {
            recipes.Add(recipe);
            return recipe.Id;
        }

        public Guid? Update(RecipeEntity recipeUpdated)
        {
            var recipeExisting = recipes.SingleOrDefault(recipe => recipe.Id == recipeUpdated.Id);
            if (recipeExisting != null)
            {
                mapper.Map(recipeUpdated, recipeExisting);
            }

            return recipeExisting?.Id;
        }

        public void Remove(Guid id)
        {
            var recipeToRemove = recipes.Single(recipe => recipe.Id.Equals(id));
            recipes.Remove(recipeToRemove);
        }
    }
}
