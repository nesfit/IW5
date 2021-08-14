using System;
using System.Collections.Generic;
using CookBook.Api.DAL.Entities;

namespace CookBook.Api.DAL.Repositories
{
    public interface IApiRepository<TEntity>
        where TEntity : IEntity
    {
        IList<TEntity> GetAll();
        TEntity? GetById(Guid id);
        Guid Insert(TEntity entity);
        Guid? Update(TEntity entity);
        void Remove(Guid id);
    }
}