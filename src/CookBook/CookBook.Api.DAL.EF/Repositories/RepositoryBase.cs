using System;
using System.Collections.Generic;
using System.Linq;
using CookBook.Api.DAL.Common.Entities.Interfaces;
using CookBook.Api.DAL.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Api.DAL.EF.Repositories
{
    public class RepositoryBase<TEntity> : IApiRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected readonly IDbContextFactory<CookBookDbContext> dbContextFactory;

        protected RepositoryBase(IDbContextFactory<CookBookDbContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public virtual IList<TEntity> GetAll()
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            return dbContext.Set<TEntity>().ToList();
        }

        public virtual TEntity? GetById(Guid id)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            return dbContext.Set<TEntity>().SingleOrDefault(entity => entity.Id == id);
        }

        public virtual Guid Insert(TEntity entity)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            var createdEntity = dbContext.Set<TEntity>().Add(entity);
            dbContext.SaveChanges();

            return createdEntity.Entity.Id;
        }

        public virtual Guid? Update(TEntity entity)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            if (dbContext.Set<TEntity>().Any(dbEntity => dbEntity.Id == entity.Id))
            {
                var updatedEntity = dbContext.Set<TEntity>().Update(entity);
                dbContext.SaveChanges();

                return updatedEntity.Entity.Id;
            }
            else
            {
                return null;
            }
        }

        public virtual void Remove(Guid id)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            var entity = dbContext.Set<TEntity>().SingleOrDefault(dbEntity => dbEntity.Id == id);
            if (entity is not null)
            {
                dbContext.Set<TEntity>().Remove(entity);
                dbContext.SaveChanges();
            }
        }
    }
}
