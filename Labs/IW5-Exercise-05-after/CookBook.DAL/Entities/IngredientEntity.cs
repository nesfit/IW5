using System.ComponentModel.DataAnnotations;
using CookBook.DAL.Entities.Base.Implementation;
using CookBook.DAL.Entities.Base.Interface;

namespace CookBook.DAL.Entities
{
    public class IngredientEntity : EntityBase, IEntity
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}