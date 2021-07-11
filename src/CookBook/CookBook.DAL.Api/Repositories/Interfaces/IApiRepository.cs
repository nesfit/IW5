using CookBook.DAL.Api.Entities;
using System;
using System.Collections.Generic;

namespace CookBook.DAL.Api.Repositories
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