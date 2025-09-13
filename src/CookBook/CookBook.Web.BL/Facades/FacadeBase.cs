﻿using System.Globalization;
using CookBook.Common;
using CookBook.Common.BL.Facades;
using CookBook.Web.BL.Mappers;
using CookBook.Web.BL.Options;
using CookBook.Web.DAL.Repositories;
using Microsoft.Extensions.Options;

namespace CookBook.Web.BL.Facades
{
    public abstract class FacadeBase<TDetailModel, TListModel> : IAppFacade
        where TDetailModel : IWithId
    {
        private readonly RepositoryBase<TDetailModel> repository;
        private readonly IWebMapper<TDetailModel, TListModel> mapper;
        private readonly LocalDbOptions localDbOptions;
        protected virtual string culture => CultureInfo.DefaultThreadCurrentCulture?.Name ?? "cs";

        protected FacadeBase(
            RepositoryBase<TDetailModel> repository,
            IWebMapper<TDetailModel, TListModel> mapper,
            IOptions<LocalDbOptions> localDbOptions)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.localDbOptions = localDbOptions.Value;
        }

        public virtual async Task<List<TListModel>> GetAllAsync()
        {
            var itemsAll = new List<TListModel>();

            if (localDbOptions.IsLocalDbEnabled)
            {
                itemsAll.AddRange(await GetAllFromLocalDbAsync());
            }
            return itemsAll;
        }

        protected async Task<IList<TListModel>> GetAllFromLocalDbAsync()
        {
            var recipesLocal = await repository.GetAllAsync();
            return mapper.ToListModels(recipesLocal);
        }

        public abstract Task<TDetailModel> GetByIdAsync(Guid id);

        public virtual async Task SaveAsync(TDetailModel data)
        {
            try
            {
                await SaveToApiAsync(data);
            }
            catch (HttpRequestException exception) when (exception.Message.Contains("Failed to fetch"))
            {
                if (localDbOptions.IsLocalDbEnabled)
                {
                    await repository.InsertAsync(data);
                }
            }
        }

        protected abstract Task<Guid> SaveToApiAsync(TDetailModel data);
        public abstract Task DeleteAsync(Guid id);

        public async Task<bool> SynchronizeLocalDataAsync()
        {
            var localItems = await repository.GetAllAsync();
            foreach (var localItem in localItems)
            {
                await SaveToApiAsync(localItem);
                await repository.RemoveAsync(localItem.Id);
            }

            return localItems.Any();
        }
    }
}
