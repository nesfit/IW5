using System;
using System.Collections.Generic;
using AutoMapper;
using CookBook.Api.DAL.Common.Entities;
using CookBook.Api.DAL.Common.Repositories;
using CookBook.Common.Models;

namespace CookBook.Api.BL.Facades
{
    public class IngredientFacade : FacadeBase<IIngredientRepository, IngredientEntity>, IIngredientFacade
    {
        private readonly IIngredientRepository ingredientRepository;
        private readonly IMapper mapper;

        public IngredientFacade(
            IIngredientRepository ingredientRepository,
            IMapper mapper) : base(ingredientRepository)
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
            return mapper.Map<IngredientDetailModel>(ingredientEntity);
        }

        public Guid Create(IngredientDetailModel ingredientModel, string? ownerId = null)
        {
            var ingredientEntity = mapper.Map<IngredientEntity>(ingredientModel);
            if (ownerId is not null)
            {
                ingredientEntity.OwnerId = ownerId;
            }
            return ingredientRepository.Insert(ingredientEntity);
        }

        public Guid? Update(IngredientDetailModel ingredientModel, string? ownerId = null)
        {
            ThrowIfWrongOwner(ingredientModel.Id, ownerId);

            var ingredientEntity = mapper.Map<IngredientEntity>(ingredientModel);
            return ingredientRepository.Update(ingredientEntity);
        }

        public void Delete(Guid id, string? ownerId = null)
        {
            ThrowIfWrongOwner(id, ownerId);

            ingredientRepository.Remove(id);
        }
    }
}
