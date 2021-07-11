﻿using AutoMapper;
using CookBook.Common.Enums;
using System;
using System.Collections.Generic;

namespace CookBook.DAL.Api.Entities
{
    public record RecipeEntity : EntityBase
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public TimeSpan Duration { get; set; }
        public FoodType FoodType { get; set; }
        public string? ImageUrl { get; set; }

        public ICollection<IngredientAmountEntity> IngredientAmounts { get; set; } = new List<IngredientAmountEntity>();
    }

    public class RecipeEntityMapperProfile : Profile
    {
        public RecipeEntityMapperProfile()
        {
            CreateMap<RecipeEntity, RecipeEntity>();
        }
    }
}