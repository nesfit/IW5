using System;
using System.ComponentModel.DataAnnotations;

namespace CookBook.Models
{
    public class IngredientDetailModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}