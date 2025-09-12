using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace CookBook.Web.DAL
{
    public class LocalDb
    {
        const string InitializeInvokeName = "LocalDb.Initialize";
        const string GetAllInvokeName = "LocalDb.GetAll";
        const string GetByIdInvokeName = "LocalDb.GetById";
        const string InsertInvokeName = "LocalDb.Insert";
        const string RemoveInvokeName = "LocalDb.Remove";

        private readonly IJSRuntime jsRuntime;

        private bool isInitialized;

        public LocalDb(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public async Task InitializeAsync()
        {
            await jsRuntime.InvokeVoidAsync(InitializeInvokeName);
            isInitialized = true;
        }

        public async Task<IList<T>> GetAllAsync<T>(string tableName)
        {
            if (!isInitialized)
            {
                await InitializeAsync();
            }

            return await jsRuntime.InvokeAsync<IList<T>>(GetAllInvokeName, tableName);
        }

        public async Task<T> GetByIdAsync<T>(string tableName, Guid id)
        {
            if (!isInitialized)
            {
                await InitializeAsync();
            }

            return await jsRuntime.InvokeAsync<T>(GetByIdInvokeName, tableName, id);
        }

        public async Task InsertAsync<T>(string tableName, T entity)
        {
            if (!isInitialized)
            {
                await InitializeAsync();
            }

            await jsRuntime.InvokeVoidAsync(InsertInvokeName, tableName, entity);
        }

        public async Task RemoveAsync(string tableName, Guid id)
        {
            if (!isInitialized)
            {
                await InitializeAsync();
            }

            await jsRuntime.InvokeVoidAsync(RemoveInvokeName, tableName, id);
        }
    }
}
