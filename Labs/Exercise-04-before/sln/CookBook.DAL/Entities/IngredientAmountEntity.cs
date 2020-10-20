using AutoMapper;
using CookBook.Models;
using System;

namespace CookBook.DAL.Entities
{
    public class IngredientAmountEntity : EntityBase
    {
        public double Amount { get; set; }
        public Unit Unit { get; set; }

        public Guid RecipeId { get; set; }
        public RecipeEntity Recipe { get; set; }

        public Guid IngredientId { get; set; }
        public IngredientEntity Ingredient { get; set; }
    }

    public class IngredientAmountEntityMapperProfile : Profile
    {
        public IngredientAmountEntityMapperProfile()
        {
            CreateMap<IngredientAmountEntity, IngredientAmountEntity>();
        }
    }
}