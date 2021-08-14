using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using CookBook.Common;
using CookBook.Common.BL.Facades;
using CookBook.Web.DAL.Repositories;

namespace CookBook.Web.BL.Facades
{
    public abstract class FacadeBase<TDetailModel, TListModel> : IAppFacade
        where TDetailModel : IWithId
    {
        private readonly RepositoryBase<TDetailModel> repository;
        private readonly IMapper mapper;
        protected virtual string apiVersion => "3";
        protected virtual string culture => "en";

        protected FacadeBase(
            RepositoryBase<TDetailModel> repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public virtual async Task<List<TListModel>> GetAllAsync()
        {
            var itemsAll = new List<TListModel>();
            itemsAll.AddRange(await GetAllFromLocalDbAsync());
            return itemsAll;
        }

        protected async Task<IList<TListModel>> GetAllFromLocalDbAsync()
        {
            var recipesLocal = await repository.GetAllAsync();
            return mapper.Map<IList<TListModel>>(recipesLocal);
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
                await repository.InsertAsync(data);
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
