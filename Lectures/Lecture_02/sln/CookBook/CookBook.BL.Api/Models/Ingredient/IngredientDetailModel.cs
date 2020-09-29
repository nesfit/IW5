using AutoMapper;
using CookBook.DAL.Entities;
using System;

namespace CookBook.BL.Api.Models.Ingredient
{
    public class IngredientDetailModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class IngredientDetailModelMapperProfile : Profile
    {
        public IngredientDetailModelMapperProfile()
        {
            CreateMap<IngredientEntity, IngredientDetailModel>();
        }
    }
}