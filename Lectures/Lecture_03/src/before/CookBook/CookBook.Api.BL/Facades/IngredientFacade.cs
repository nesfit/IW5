using System;
using System.Collections.Generic;
using CookBook.Api.BL.Mappers;
using CookBook.Api.DAL.Common.Entities;
using CookBook.Api.DAL.Common.Repositories;
using CookBook.Common.Models;

namespace CookBook.Api.BL.Facades
{
    public class IngredientFacade : IIngredientFacade
    {
        private readonly IIngredientRepository ingredientRepository;
        private readonly IngredientMapper mapper;

        public IngredientFacade(
            IIngredientRepository ingredientRepository,
            IngredientMapper mapper)
        {
            this.ingredientRepository = ingredientRepository;
            this.mapper = mapper;
        }

        public List<IngredientListModel> GetAll()
        {
            return mapper.ToListModels(ingredientRepository.GetAll());
        }

        public IngredientDetailModel? GetById(Guid id)
        {
            var ingredientEntity = ingredientRepository.GetById(id);
            return ingredientEntity == null
                ? null
                : mapper.ToDetailModel(ingredientEntity);
        }

        public Guid CreateOrUpdate(IngredientDetailModel ingredientModel)
        {
            return ingredientRepository.Exists(ingredientModel.Id)
                ? Update(ingredientModel)!.Value
                : Create(ingredientModel);
        }

        public Guid Create(IngredientDetailModel ingredientModel)
        {
            var ingredientEntity = mapper.ToEntity(ingredientModel);
            return ingredientRepository.Insert(ingredientEntity);
        }

        public Guid? Update(IngredientDetailModel ingredientModel)
        {
            var ingredientEntity = mapper.ToEntity(ingredientModel);
            return ingredientRepository.Update(ingredientEntity);
        }

        public void Delete(Guid id)
        {
            ingredientRepository.Remove(id);
        }
    }
}
