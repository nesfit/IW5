using AutoMapper;
using System;

namespace CookBook.DAL.Api.Entities
{
    public record IngredientEntity : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        
        public IngredientEntity(Guid id, string name, string description, string? imageUrl = null)
            : base(id)
        {
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
        }
    }

    public class IngredientEntityMapperProfile : Profile
    {
        public IngredientEntityMapperProfile()
        {
            CreateMap<IngredientEntity, IngredientEntity>();
        }
    }
}
