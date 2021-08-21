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
        protected readonly CookBookDbContext dbContext;

        protected RepositoryBase(CookBookDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public virtual IList<TEntity> GetAll()
        {
            return dbContext.Set<TEntity>().ToList();
        }

        public virtual TEntity? GetById(Guid id)
        {
            return dbContext.Set<TEntity>().SingleOrDefault(entity => entity.Id == id);
        }

        public virtual Guid Insert(TEntity entity)
        {
            var createdEntity = dbContext.Set<TEntity>().Add(entity);
            dbContext.SaveChanges();

            return createdEntity.Entity.Id;
        }

        public virtual Guid? Update(TEntity entity)
        {
            if (dbContext.Set<TEntity>().Any(dbEntity => dbEntity.Id == entity.Id))
            {
                dbContext.Set<TEntity>().Attach(entity);
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
            var entity = GetById(id);
            if (entity is not null)
            {
                dbContext.Set<TEntity>().Remove(entity);
                dbContext.SaveChanges();
            }
        }

        public virtual bool Exists(Guid id)
        {
            return dbContext.Set<TEntity>().Any(entity => entity.Id == id);
        }
    }
}
