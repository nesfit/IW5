using System;
using System.Collections.Generic;
using CookBook.Api.DAL.Common.Entities;
using CookBook.Api.DAL.Common.Repositories;

namespace CookBook.Api.DAL.EF.Repositories
{
    public class IngredientAmountRepository : IIngredientAmountRepository
    {
        public IList<IngredientAmountEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public IngredientAmountEntity? GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Guid Insert(IngredientAmountEntity entity)
        {
            throw new NotImplementedException();
        }

        public Guid? Update(IngredientAmountEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public IList<IngredientAmountEntity> GetByRecipeId(Guid recipeId)
        {
            throw new NotImplementedException();
        }

        public IngredientAmountEntity? GetByRecipeIdAndIngredientId(Guid recipeId, Guid ingredientId)
        {
            throw new NotImplementedException();
        }
    }
}
