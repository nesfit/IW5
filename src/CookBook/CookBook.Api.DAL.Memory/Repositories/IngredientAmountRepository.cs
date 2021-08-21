using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CookBook.Api.DAL.Common.Entities;
using CookBook.Api.DAL.Common.Repositories;

namespace CookBook.Api.DAL.Memory.Repositories
{
    public class IngredientAmountRepository : IIngredientAmountRepository
    {
        private readonly IList<IngredientAmountEntity> ingredientAmounts;
        private readonly IMapper mapper;

        public IngredientAmountRepository(
            Storage storage,
            IMapper mapper)
        {
            ingredientAmounts = storage.IngredientAmounts;
            this.mapper = mapper;
        }

        public IList<IngredientAmountEntity> GetAll()
        {
            return ingredientAmounts;
        }

        public IngredientAmountEntity? GetById(Guid id)
        {
            return ingredientAmounts.SingleOrDefault(ingredientAmount => ingredientAmount.Id == id);
        }

        public Guid Insert(IngredientAmountEntity ingredientAmount)
        {
            ingredientAmounts.Add(ingredientAmount);
            return ingredientAmount.Id;
        }

        public Guid? Update(IngredientAmountEntity entity)
        {
            var ingredientAmountExisting = ingredientAmounts.SingleOrDefault(ingredientAmountInStorage =>
                ingredientAmountInStorage.Id == entity.Id);
            if (ingredientAmountExisting != null)
            {
                mapper.Map(entity, ingredientAmountExisting);
            }

            return ingredientAmountExisting?.Id;
        }

        public void Remove(Guid id)
        {
            var ingredientAmountToRemove = ingredientAmounts.Single(ingredientAmount => ingredientAmount.Id.Equals(id));
            ingredientAmounts.Remove(ingredientAmountToRemove);
        }

        public bool Exists(Guid id)
        {
            return ingredientAmounts.Any(ingredientAmount => ingredientAmount.Id == id);
        }
    }
}
