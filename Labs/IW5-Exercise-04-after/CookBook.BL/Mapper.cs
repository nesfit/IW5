namespace CookBook.BL
{
    using System.Collections.Generic;

    using CookBook.BL.Models;
    using CookBook.DAL.Entities;

    public class Mapper
    {
        public RecipeListModel Map(RecipeEntity entity)
        {
            return new RecipeListModel
                       {
                           Id = entity.Id,
                           Name = entity.Name,
                           Type = entity.Type
                       };
        }

        public ICollection<RecipeListModel> Map(IEnumerable<RecipeEntity> entities)
        {
            var models = new List<RecipeListModel>();

            foreach (var entity in entities)
            {
                models.Add(Map(entity));
            }

            return models;
        }

        public RecipeDetailModel MapDetail(RecipeEntity entity)
        {
            return new RecipeDetailModel
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