using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CookBook.Common;

namespace CookBook.Web.DAL.Repositories
{
    public abstract class RepositoryBase<T> : IWebRepository<T>
        where T : IWithId
    {
        private readonly LocalDb localDb;
        public abstract string TableName { get; }

        protected RepositoryBase(LocalDb localDb)
        {
            this.localDb = localDb;
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await localDb.GetAllAsync<T>(TableName);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await localDb.GetByIdAsync<T>(TableName, id);
        }

        public async Task InsertAsync(T entity)
        {
            await localDb.InsertAsync(TableName, entity);
        }

        public async Task RemoveAsync(Guid id)
        {
            await localDb.RemoveAsync(TableName, id);
        }
    }
}
