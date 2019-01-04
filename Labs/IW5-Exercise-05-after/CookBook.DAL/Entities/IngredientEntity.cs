using CookBook.DAL.Entities.Base.Implementation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookBook.DAL.Entities
{
    [Table("Ingredients")]
    public class IngredientEntity : EntityBase
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}