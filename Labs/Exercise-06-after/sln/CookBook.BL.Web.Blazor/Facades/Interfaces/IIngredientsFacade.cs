using CookBook.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CookBook.BL.Web.Blazor.Facades
{
    public interface IIngredientsFacade
    {
        Task<IList<IngredientListModel>> IngredientsGetAsync(string culture);
        Task<IngredientDetailModel> IngredientGetAsync(Guid id, string culture);
        Task SaveAsync(IngredientDetailModel data, string culture);
    }
}