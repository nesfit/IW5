using AutoMapper;
using CookBook.BL.Common.Facades;
using CookBook.DAL.Api.Entities;
using CookBook.DAL.Api.Repositories;
using CookBook.Models;
using System;
using System.Collections.Generic;

namespace CookBook.BL.Api.Facades
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

        public IngredientDetailModel GetById(Guid id)
        {
            return mapper.Map<IngredientDetailModel>(ingredientRepository.GetById(id));
        }

        public Guid Create(IngredientDetailModel ingredient)
        {
            var ingredientEntity = mapper.Map<IngredientEntity>(ingredient);
            return ingredientRepository.Insert(ingredientEntity);
        }

        public Guid? Update(IngredientDetailModel ingredient)
        {
            var ingredientEntity = mapper.Map<IngredientEntity>(ingredient);
            return ingredientRepository.Update(ingredientEntity);
        }

        public void Delete(Guid id)
        {
            ingredientRepository.Remove(id);
        }
    }
}
