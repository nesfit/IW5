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
        Guid Create(IngredientDetailModel ingredientModel, string? ownerId = null);
        Guid? Update(IngredientDetailModel ingredientModel, string? ownerId = null);
        void Delete(Guid id, string? ownerId = null);
    }
}
