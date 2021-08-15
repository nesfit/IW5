using System;
using System.Collections.Generic;
using System.Linq;
using CookBook.Api.DAL.Common.Entities;
using CookBook.Api.DAL.Common.Repositories;

namespace CookBook.Api.DAL.EF.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly Func<CookBookDbContext> dbContextFactory;

        public IngredientRepository(Func<CookBookDbContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public IList<IngredientEntity> GetAll()
        {
            using var dbContext = dbContextFactory();
            return dbContext.Ingredients.ToList();
        }

        public IngredientEntity? GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Guid Insert(IngredientEntity entity)
        {
            throw new NotImplementedException();
        }

        public Guid? Update(IngredientEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
