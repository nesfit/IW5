using CookBook.BL.Models;
using CookBook.DAL.Entities;

namespace CookBook.BL
{
    internal class Mapper
    {
        public RecipeListModel MapEntityToListModel(RecipeEntity entity)
        {
            return entity == null
                ? null
                : new RecipeListModel()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Type = entity.Type,
                    Duration = entity.Duration
                };
        }

        public RecipeDetailModel MapEntityToDetailModel(RecipeEntity entity)
        {
            return entity == null
                ? null
                : new RecipeDetailModel()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Type = entity.Type,
                    Description = entity.Description,
                    Duration = entity.Duration,
                    Ingredients = entity.Ingredients
                };
        }

        public RecipeEntity MapDetailModelToEntity(RecipeDetailModel entity)
        {
            return entity == null
                ? null
                : new RecipeEntity()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Type = entity.Type,
                    Description = entity.Description,
                    Duration = entity.Duration,
                    Ingredients = entity.Ingredients
                };
        }
    }
}
