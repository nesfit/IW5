using AutoMapper;

namespace CookBook.DAL.Entities
{
    public class IngredientEntity : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class IngredientEntityMapperProfile : Profile
    {
        public IngredientEntityMapperProfile()
        {
            CreateMap<IngredientEntity, IngredientEntity>();
        }
    }
}