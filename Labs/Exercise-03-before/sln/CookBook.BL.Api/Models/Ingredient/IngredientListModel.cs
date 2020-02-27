using AutoMapper;
using CookBook.DAL.Entities;
using System;

namespace CookBook.BL.Api.Models.Ingredient
{
    public class IngredientListModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class IngredientListModelMapperProfile : Profile
    {
        public IngredientListModelMapperProfile()
        {
            CreateMap<IngredientEntity, IngredientListModel>();
        }
    }
}