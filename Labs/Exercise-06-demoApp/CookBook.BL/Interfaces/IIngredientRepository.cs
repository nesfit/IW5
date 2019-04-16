using System;
using System.Collections.Generic;
using CookBook.BL.Models;

namespace CookBook.BL.Interfaces
{
    public interface IIngredientRepository
    {
        IEnumerable<IngredientListModel> GetAll();
        IngredientDetailModel GetById(Guid id);
        IngredientDetailModel Create(IngredientDetailModel model);
        void Update(IngredientDetailModel model);
        void Delete(Guid id);
    }
}
