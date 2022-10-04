using System;
using AutoMapper;

namespace CookBook.Api.DAL.Common.Entities
{
    public record IngredientEntity : EntityBase
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public string? ImageUrl { get; set; }
    }

    public class IngredientEntityMapperProfile : Profile
    {
        public IngredientEntityMapperProfile()
        {
            CreateMap<IngredientEntity, IngredientEntity>();
        }
    }
}
