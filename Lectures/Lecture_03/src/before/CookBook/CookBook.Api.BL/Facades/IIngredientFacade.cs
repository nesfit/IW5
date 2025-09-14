using System;
using System.Collections.Generic;
using CookBook.Common.BL.Facades;
using CookBook.Common.Models;

namespace CookBook.Api.BL.Facades
{
    public interface IIngredientFacade : IAppFacade
    {
        List<IngredientListModel> GetAll();
        IngredientDetailModel? GetById(Guid id);
        Guid CreateOrUpdate(IngredientDetailModel ingredientModel);
        Guid Create(IngredientDetailModel ingredientModel);
        Guid? Update(IngredientDetailModel ingredientModel);
        void Delete(Guid id);
    }
}
