using System;
using System.Collections.Generic;
using AutoMapper;
using CookBook.Api.DAL.Common.Entities;
using CookBook.Api.DAL.Common.Repositories;
using CookBook.Common.BL.Facades;
using CookBook.Common.Models;

namespace CookBook.Api.BL.Facades
{
    public class IngredientFacade : IAppFacade
    {
        private readonly IIngredientRepository ingredientRepository;
        private readonly IMapper mapper;

        public IngredientFacade(
            IIngredientRepository ingredientRepository,
            IMapper mapper)
        {
            this.ingredientRepository = ingredientRepository;
            this.mapper = mapper;
        }

        public List<IngredientListModel> GetAll()
        {
            return mapper.Map<List<IngredientListModel>>(ingredientRepository.GetAll());
        }

        public IngredientDetailModel? GetById(Guid id)
        {
            var ingredientEntity = ingredientRepository.GetById(id);
            return ingredientEntity is null
                ? null
                : mapper.Map<IngredientDetailModel>(ingredientEntity);
        }

        public Guid CreateOrUpdate(IngredientDetailModel ingredientModel)
        {
            var existingEntity = ingredientRepository.GetById(ingredientModel.Id);
            return existingEntity is null
                ? Create(ingredientModel)
                : Update(ingredientModel)!.Value;
        }

        public Guid Create(IngredientDetailModel ingredientModel)
        {
            var ingredientEntity = mapper.Map<IngredientEntity>(ingredientModel);
            return ingredientRepository.Insert(ingredientEntity);
        }

        

        public Guid? Update(IngredientDetailModel ingredientModel)
        {
            var ingredientEntity = mapper.Map<IngredientEntity>(ingredientModel);
            return ingredientRepository.Update(ingredientEntity);
        }

        public void Delete(Guid id)
        {
            ingredientRepository.Remove(id);
        }
    }
}
