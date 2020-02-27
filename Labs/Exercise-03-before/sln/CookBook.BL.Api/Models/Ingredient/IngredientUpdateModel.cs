using AutoMapper;
using CookBook.DAL.Entities;
using System;

namespace CookBook.BL.Api.Models.Ingredient
{
    public class IngredientUpdateModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class IngredientUpdateModelMapperProfile : Profile
    {
        public IngredientUpdateModelMapperProfile()
        {
            CreateMap<IngredientUpdateModel, IngredientEntity>();
        }
    }
}