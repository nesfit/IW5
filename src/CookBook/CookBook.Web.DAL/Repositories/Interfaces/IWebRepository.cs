using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CookBook.Common;

namespace CookBook.Web.DAL.Repositories
{
    public interface IWebRepository<T>
        where T : IWithId
    {
        string TableName { get; }
        Task<IList<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task InsertAsync(T entity);
        Task RemoveAsync(Guid id);
    }
}
