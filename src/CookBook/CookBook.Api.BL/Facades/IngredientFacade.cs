using System;
using System.Collections.Generic;
using Mapster;
using CookBook.Api.DAL.Common.Entities;
using CookBook.Api.DAL.Common.Repositories;
using CookBook.Common.Models;

namespace CookBook.Api.BL.Facades
{
    public class IngredientFacade : IIngredientFacade
    {
        private readonly IIngredientRepository ingredientRepository;

        public IngredientFacade(
            IIngredientRepository ingredientRepository)
        {
            this.ingredientRepository = ingredientRepository;
        }

        public List<IngredientListModel> GetAll()
        {
            return ingredientRepository.GetAll().Adapt<List<IngredientListModel>>();
        }

        public IngredientDetailModel? GetById(Guid id)
        {
            var ingredientEntity = ingredientRepository.GetById(id);
            return ingredientEntity?.Adapt<IngredientDetailModel>();
        }

        public Guid CreateOrUpdate(IngredientDetailModel ingredientModel)
        {
            return ingredientRepository.Exists(ingredientModel.Id)
                ? Update(ingredientModel)!.Value
                : Create(ingredientModel);
        }

        public Guid Create(IngredientDetailModel ingredientModel)
        {
            var ingredientEntity = ingredientModel.Adapt<IngredientEntity>();
            return ingredientRepository.Insert(ingredientEntity);
        }

        public Guid? Update(IngredientDetailModel ingredientModel)
        {
            var ingredientEntity = ingredientModel.Adapt<IngredientEntity>();
            return ingredientRepository.Update(ingredientEntity);
        }

        public void Delete(Guid id)
        {
            ingredientRepository.Remove(id);
        }
    }
}
