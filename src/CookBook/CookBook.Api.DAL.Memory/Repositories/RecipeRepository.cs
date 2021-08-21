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

        public bool Exists(Guid id)
        {
            return recipes.Any(recipe => recipe.Id == id);
        }
    }
}
