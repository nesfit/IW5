using System;
using System.Collections.Generic;
using CookBook.Common.BL.Facades;
using CookBook.Common.Models;

namespace CookBook.Api.BL.Facades
{
    public interface IRecipeFacade : IAppFacade
    {
        List<RecipeListModel> GetAll();
        RecipeDetailModel? GetById(Guid id);
        Guid CreateOrUpdate(RecipeDetailModel recipeModel);
        Guid Create(RecipeDetailModel recipeModel);
        Guid? Update(RecipeDetailModel recipeModel);
        void Delete(Guid id);
    }
}
