using System;
using System.Collections.Generic;
using CookBook.Api.DAL.Common.Entities;
using CookBook.Api.DAL.Common.Repositories;

namespace CookBook.Api.DAL.EF.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        public IList<RecipeEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public RecipeEntity? GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Guid Insert(RecipeEntity entity)
        {
            throw new NotImplementedException();
        }

        public Guid? Update(RecipeEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
