using System;
using System.Collections.Generic;
using System.Linq;
using CookBook.Api.DAL.Common.Entities.Interfaces;
using CookBook.Api.DAL.Common.Repositories;

namespace CookBook.Api.DAL.EF.Repositories
{
    public class RepositoryBase<TEntity> : IApiRepository<TEntity>, IDisposable
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
            var existingEntity = dbContext.Set<TEntity>().Find(entity.Id);
            if (existingEntity is null)
            {
                return null;
            }

            var ownerId = existingEntity.OwnerId;
            dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
            existingEntity.OwnerId = ownerId;
            dbContext.SaveChanges();

            return existingEntity.Id;
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

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
