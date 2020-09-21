using System;
using CookBook.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CookBook.BL.Web.Blazor.Facades
{
    public interface IRecipesFacade
    {
        Task<IList<RecipeListModel>> RecipesGetAsync(string culture);
        Task<RecipeDetailModel> RecipeGetAsync(Guid id, string culture);
        Task<Guid> SaveAsync(RecipeDetailModel data, string culture);
        Task DeleteAsync(Guid id, string culture);
    }
}