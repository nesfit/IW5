using CookBook.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CookBook.DAL.Web.Repositories
{
    public interface IWebRepository<T>
        where T : IId
    {
        string TableName { get; }
        Task<IList<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task InsertAsync(T entity);
        Task RemoveAsync(Guid id);
    }
}